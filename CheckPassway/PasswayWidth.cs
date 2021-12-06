using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using Autodesk.Revit.DB;
using System.Text.RegularExpressions;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System.Net.Http;
using System.Globalization;

namespace CheckPassway
{
    public partial class PasswayWidth : System.Windows.Forms.Form
    {
        private RevitDocument m_connect = null;
        public List<Element> select_roomList = new List<Element>();
        public List<Element> other_roomList = new List<Element>();
        public List<Element> check_dimensionList = new List<Element>();
        public List<Element> check_roomList = new List<Element>();
        ExternalEvent m_CheckDimension_ExternalEvent;
        public static bool login_result = false;
        public static HttpClient client = new HttpClient();
        public PasswayWidth(RevitDocument connect, ExternalEvent check_dimension_event)
        {
            m_CheckDimension_ExternalEvent = check_dimension_event;
            m_connect = connect;
            InitializeComponent();
            room_groupBox.Visible = false;
            dimension_listView.Visible = false;
            description_label.Visible = false;
            outcome_label.Visible = false;
            selection_button.Visible = false;
        }

        private void check_button_Click(object sender, EventArgs e)
        {
            if (passway_width_textBox.Text == "")
            {
                MessageBox.Show("請輸入通道寬度");
                return;
            }
            if (room_checkBox.Checked == false && dimension_checkBox.Checked == false)
            {
                MessageBox.Show("請選擇檢查項目");
                return;
            }
            room_groupBox.Visible = false;
            dimension_listView.Clear();
            check_dimensionList.Clear();
            check_roomList.Clear();
            room_checkBox.Enabled = false;
            dimension_checkBox.Enabled = false;
            check_button.Enabled = false;
            Autodesk.Revit.DB.View view = m_connect.RevitDoc.Document.ActiveView;
            if (dimension_checkBox.Checked == true)
            {
                FilteredElementCollector dimensions = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Dimensions).OfClass(typeof(Dimension));
                foreach (Element dimension in dimensions)
                {
                    if (dimension.Name == "Sino_dimension")
                    {
                        double total_length = Convert.ToDouble(dimension.get_Parameter(BuiltInParameter.DIM_TOTAL_LENGTH).AsValueString());
                        if (total_length < Convert.ToDouble(passway_width_textBox.Text))
                        {
                            Dimension di = dimension as Dimension;
                            Line di_line = di.Curve as Line;
                            XYZ middle_point = di_line.Origin;
                            Room temp_room = m_connect.RevitDoc.Document.GetRoomAtPoint(middle_point);
                            /*if (temp_room != null)
                            {
                                check_dimensionList.Add(dimension);
                                check_roomList.Add(temp_room);
                            }*/
                            check_dimensionList.Add(dimension);
                            check_roomList.Add(temp_room);
                        }
                    }
                }
            }
            if (room_checkBox.Checked == true)
            {
                if (select_roomList.Count > 0)
                {
                    for (int i=0; i<select_roomList.Count; i++)
                    {
                        Element room = select_roomList[i];
                        // use room segments
                        Room r = room as Room;
                        /*SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
                        opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
                        IList<IList<BoundarySegment>> bs_List = r.GetBoundarySegments(opt).ToList();
                        MessageBox.Show(bs_List[0].Count.ToString());*/
                        // use solid filter
                        /*Options option = m_connect.RevitDoc.Application.Application.Create.NewGeometryOptions();
                        option.ComputeReferences = true;
                        option.DetailLevel = ViewDetailLevel.Fine;
                        GeometryElement geo_elem = room.get_Geometry(option);
                        Solid s = null;
                        foreach (GeometryObject geo_object in geo_elem)
                        {
                            Solid room_s = geo_object as Solid;
                            if (room_s != null)
                            {
                                s = room_s;
                            }
                        }
                        FilteredElementCollector a_dimension_collector = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Dimensions).OfClass(typeof(Dimension));
                        a_dimension_collector = a_dimension_collector.WherePasses(new ElementIntersectsSolidFilter(s));
                        MessageBox.Show(a_dimension_collector.Count().ToString());*/

                        BoundingBoxXYZ bd = room.get_BoundingBox(view);
                        FilteredElementCollector dimension_collector = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Dimensions).OfClass(typeof(Dimension));
                        foreach (Dimension di in dimension_collector)
                        {
                            if (di.DimensionShape == DimensionShape.Linear)
                            {
                                if (dimension_checkBox.Checked == true && di.Name == "Sino_dimension")
                                {
                                    continue;
                                }
                                Line di_line = di.Curve as Line;
                                XYZ middle_point = di_line.Origin;
                                if (middle_point.X <= bd.Max.X && middle_point.Y <= bd.Max.Y && middle_point.Z <= bd.Max.Z && middle_point.X >= bd.Min.X && middle_point.Y >= bd.Min.Y && middle_point.Z >= bd.Min.Z)
                                {
                                    Room temp_room = m_connect.RevitDoc.Document.GetRoomAtPoint(middle_point);
                                    if (temp_room != null && r.Id == temp_room.Id)
                                    {
                                        ReferenceArray ref_array = di.References;
                                        bool result = false;
                                        XYZ direction = new XYZ();
                                        Element side_wall = null;
                                        foreach (Reference reference in ref_array)
                                        {
                                            Element ref_elem = m_connect.RevitDoc.Document.GetElement(reference.ElementId);
                                            Category wall_category = Category.GetCategory(m_connect.RevitDoc.Document, BuiltInCategory.OST_Walls);
                                            if (ref_elem.Category.Id == wall_category.Id)
                                            {
                                                LocationCurve wall_loc = ref_elem.Location as LocationCurve;
                                                Line wall_line = wall_loc.Curve as Line;
                                                XYZ direction_point = wall_line.Direction;
                                                if (side_wall != null)
                                                {
                                                    if (Math.Round(direction_point.X, 2).ToString() == Math.Round(direction.X, 2).ToString() && side_wall.Id != ref_elem.Id)
                                                    {
                                                        result = true;
                                                    }
                                                }
                                                side_wall = ref_elem;
                                                direction = direction_point;
                                            }
                                        }
                                        if (result == true)
                                        {
                                            double total_length = Convert.ToDouble(di.get_Parameter(BuiltInParameter.DIM_TOTAL_LENGTH).AsValueString());
                                            if (total_length < Convert.ToDouble(passway_width_textBox.Text))
                                            {
                                                check_dimensionList.Add(di);
                                                check_roomList.Add(r);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //MessageBox.Show(check_dimensionList.Count.ToString());
                }
            }
            if (check_dimensionList.Count > 0)
            {
                // initialize the element listview
                dimension_listView.Columns.Add("ID", 90);
                dimension_listView.Columns.Add("房間名稱", 180);
                dimension_listView.Columns.Add("標註寬度(m)", 120);
                for (int i = 0; i < check_dimensionList.Count; i++)
                {
                    Element check_di = check_dimensionList[i];
                    Element check_room = check_roomList[i];
                    if (check_room == null)
                    {
                        string[] element_list_value = { check_di.Id.ToString(), "無", check_di.get_Parameter(BuiltInParameter.DIM_TOTAL_LENGTH).AsValueString() };
                        dimension_listView.Items.Add(new ListViewItem(element_list_value));
                    }
                    else
                    {
                        string[] element_list_value = { check_di.Id.ToString(), check_room.Name, check_di.get_Parameter(BuiltInParameter.DIM_TOTAL_LENGTH).AsValueString() };
                        dimension_listView.Items.Add(new ListViewItem(element_list_value));
                    }
                }
                description_label.Location = new System.Drawing.Point(description_label.Location.X, option_label.Location.Y + 70);
                selection_button.Location = new System.Drawing.Point(description_label.Location.X + 282, option_label.Location.Y + 58);
                dimension_listView.Location = new System.Drawing.Point(dimension_listView.Location.X, description_label.Location.Y + 25);
                selection_button.Visible = true;
                dimension_listView.Visible = true;
                description_label.Visible = true;
                outcome_label.Visible = false;
            }
            else
            {
                outcome_label.Location = new System.Drawing.Point(outcome_label.Location.X, option_label.Location.Y + 60);
                selection_button.Location = new System.Drawing.Point(outcome_label.Location.X + 265, outcome_label.Location.Y + 2);
                selection_button.Visible = true;
                dimension_listView.Visible = false;
                description_label.Visible = false;
                outcome_label.Visible = true;
            }
        }

        private void room_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (room_checkBox.Checked == true)
            {
                if (select_roomList.Count != 0)
                {
                    room_groupBox.Visible = true;
                    return;
                }
                FilteredElementCollector rooms = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Rooms);
                foreach (Element room in rooms)
                {
                    if (room.Name.Contains("走道") || room.Name.Contains("通道"))
                    {
                        select_roomList.Add(room);
                    }
                    else
                    {
                        other_roomList.Add(room);
                    }
                }
                if (select_roomList.Count != 0)
                {
                    for (int i=0; i<select_roomList.Count; i++)
                    {
                        Element room_elem = select_roomList[i];
                        string room_name = room_elem.Name;
                        if (i == 0)
                        {
                            Label room_label_s = new Label();
                            room_label_s.Name = "room_label_s" + i.ToString();
                            room_label_s.Text = room_name;
                            room_label_s.BorderStyle = BorderStyle.FixedSingle;
                            room_label_s.Location = new System.Drawing.Point(selected_label.Location.X, selected_label.Location.Y + 50);
                            room_label_s.Font = selected_label.Font;
                            room_label_s.Parent = room_groupBox;
                            room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                            room_label_s.Padding = new Padding(3);
                            room_label_s.Cursor = Cursors.Hand;
                            room_label_s.Width = 150;
                            room_label_s.Height = 30;
                            room_label_s.Click += create_label_Click;
                            this.room_groupBox.Controls.Add(room_label_s);
                        }
                        else
                        {
                            if (i%3 == 0)
                            {
                                System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i-3).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                                Label room_label_s = new Label();
                                room_label_s.Name = "room_label_s" + i.ToString();
                                room_label_s.Text = room_name;
                                room_label_s.BorderStyle = BorderStyle.FixedSingle;
                                room_label_s.Location = new System.Drawing.Point(previous_label.Location.X, previous_label.Location.Y + 40);
                                room_label_s.Font = selected_label.Font;
                                room_label_s.Parent = room_groupBox;
                                room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                                room_label_s.Padding = new Padding(3);
                                room_label_s.Cursor = Cursors.Hand;
                                room_label_s.Width = 150;
                                room_label_s.Height = 30;
                                room_label_s.Click += create_label_Click;
                                this.room_groupBox.Controls.Add(room_label_s);
                            }
                            else
                            {
                                System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i - 1).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                                Label room_label_s = new Label();
                                room_label_s.Name = "room_label_s" + i.ToString();
                                room_label_s.Text = room_name;
                                room_label_s.BorderStyle = BorderStyle.FixedSingle;
                                room_label_s.Location = new System.Drawing.Point(previous_label.Location.X + previous_label.Width + 20, previous_label.Location.Y);
                                room_label_s.Font = selected_label.Font;
                                room_label_s.Parent = room_groupBox;
                                room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                                room_label_s.Padding = new Padding(3);
                                room_label_s.Cursor = Cursors.Hand;
                                room_label_s.Width = 150;
                                room_label_s.Height = 30;
                                room_label_s.Click += create_label_Click;
                                this.room_groupBox.Controls.Add(room_label_s);
                            }
                        }
                        room_groupBox.Visible = true;
                    }
                }
                for (int i=0; i<other_roomList.Count; i++)
                {
                    Element un_room_elem = other_roomList[i];
                    room_comboBox.Items.Add(un_room_elem.Name);
                }
            }
            else
            {
                room_groupBox.Visible = false;
            }
        }

        private void accept_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void PasswayWidth_Load(object sender, EventArgs e)
        {
            select_roomList.Clear();
            other_roomList.Clear();
            if (LoginForm.external == false)
            {
                client = client_login();
                if (client == null)
                {
                    this.Text = "通道寬度檢核 - 認證失敗，5秒後強制關閉";
                    this.Enabled = false;
                    login_result = false;
                    timer1.Start();
                }
                var result = client.GetAsync($"/user/me").Result;
                if (result.IsSuccessStatusCode)
                {
                    login_result = true;
                    string s = result.Content.ReadAsStringAsync().Result;
                    this.Text = "通道寬度檢核 - " + DecodeEncodedNonAsciiCharacters(s.Substring(8, s.Length - 10));
                }
                else
                {
                    this.Text = "通道寬度檢核 - 認證失敗，5秒後強制關閉";
                    this.Enabled = false;
                    login_result = false;
                    timer1.Start();
                }
            }
            else
            {
                this.Text = "通道寬度檢核 - " + LoginForm.external_username;
            }
        }

        public static HttpClient client_login()
        {
            try
            {
                client = new HttpClient();
                //client.BaseAddress = new Uri("http://127.0.0.1:8000/");
                client.BaseAddress = new Uri("https://bimdata.sinotech.com.tw/");
                //client.BaseAddress = new Uri("http://bimdata.secltd/");
                client.DefaultRequestHeaders.Accept.Clear();
                var headerValue = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(headerValue);
                client.DefaultRequestHeaders.ConnectionClose = true;
                Task.WaitAll(client.GetAsync($"/login/?USERNAME={Environment.UserName}&REVITAPI=SinoStation-Passway"));
                //Task.WaitAll(client.GetAsync($"/login/?USERNAME=11111&REVITAPI=SinoPipe"));
                return client;
            }
            catch (Exception)
            {
                login_result = false;
                return null;
            }
        }

        private void create_label_Click(object sender, EventArgs e)
        {
            FilteredElementCollector rooms = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Rooms);
            foreach (Element room in rooms)
            {
                if (room.Name == (sender as Label).Text)
                {
                    select_roomList.RemoveAll(l => l.Id == room.Id);
                    other_roomList.Add(room);
                    break;
                }
            }
            List<System.Windows.Forms.Label> exist_labelList = new List<Label>();
            foreach (System.Windows.Forms.Label exist_label in this.room_groupBox.Controls.OfType<Label>())
            {
                if (exist_label.Name.Contains("room_label_s"))
                {
                    exist_labelList.Add(exist_label);
                }
            }
            foreach (System.Windows.Forms.Label exist_label in exist_labelList)
            {
                this.room_groupBox.Controls.Remove(exist_label);
            }
            room_comboBox.Items.Clear();
            room_comboBox.Text = "選擇需要新增檢查的房間";
            for (int i=0; i<select_roomList.Count; i++)
            {
                Element room_elem = select_roomList[i];
                string room_name = room_elem.Name;
                if (i == 0)
                {
                    Label room_label_s = new Label();
                    room_label_s.Name = "room_label_s" + i.ToString();
                    room_label_s.Text = room_name;
                    room_label_s.BorderStyle = BorderStyle.FixedSingle;
                    room_label_s.Location = new System.Drawing.Point(selected_label.Location.X, selected_label.Location.Y + 50);
                    room_label_s.Font = selected_label.Font;
                    room_label_s.Parent = room_groupBox;
                    room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                    room_label_s.Padding = new Padding(3);
                    room_label_s.Cursor = Cursors.Hand;
                    room_label_s.Width = 150;
                    room_label_s.Height = 30;
                    room_label_s.Click += create_label_Click;
                    this.room_groupBox.Controls.Add(room_label_s);
                }
                else
                {
                    if (i % 3 == 0)
                    {
                        System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i - 3).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                        Label room_label_s = new Label();
                        room_label_s.Name = "room_label_s" + i.ToString();
                        room_label_s.Text = room_name;
                        room_label_s.BorderStyle = BorderStyle.FixedSingle;
                        room_label_s.Location = new System.Drawing.Point(previous_label.Location.X, previous_label.Location.Y + 40);
                        room_label_s.Font = selected_label.Font;
                        room_label_s.Parent = room_groupBox;
                        room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                        room_label_s.Padding = new Padding(3);
                        room_label_s.Cursor = Cursors.Hand;
                        room_label_s.Width = 150;
                        room_label_s.Height = 30;
                        room_label_s.Click += create_label_Click;
                        this.room_groupBox.Controls.Add(room_label_s);
                    }
                    else
                    {
                        System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i - 1).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                        Label room_label_s = new Label();
                        room_label_s.Name = "room_label_s" + i.ToString();
                        room_label_s.Text = room_name;
                        room_label_s.BorderStyle = BorderStyle.FixedSingle;
                        room_label_s.Location = new System.Drawing.Point(previous_label.Location.X + previous_label.Width + 20, previous_label.Location.Y);
                        room_label_s.Font = selected_label.Font;
                        room_label_s.Parent = room_groupBox;
                        room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                        room_label_s.Padding = new Padding(3);
                        room_label_s.Cursor = Cursors.Hand;
                        room_label_s.Width = 150;
                        room_label_s.Height = 30;
                        room_label_s.Click += create_label_Click;
                        this.room_groupBox.Controls.Add(room_label_s);
                    }
                }
            }
            for (int i = 0; i < other_roomList.Count; i++)
            {
                Element un_room_elem = other_roomList[i];
                room_comboBox.Items.Add(un_room_elem.Name);
            }
        }

        private void room_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select_name = room_comboBox.SelectedItem.ToString();
            FilteredElementCollector rooms = new FilteredElementCollector(m_connect.RevitDoc.Document).OfCategory(BuiltInCategory.OST_Rooms);
            foreach (Element room in rooms)
            {
                if (room.Name == select_name)
                {
                    other_roomList.RemoveAll(l => l.Id == room.Id);
                    select_roomList.Add(room);
                    break;
                }
            }
            List<System.Windows.Forms.Label> exist_labelList = new List<Label>();
            foreach (System.Windows.Forms.Label exist_label in this.room_groupBox.Controls.OfType<Label>())
            {
                if (exist_label.Name.Contains("room_label_s"))
                {
                    exist_labelList.Add(exist_label);
                }
            }
            foreach (System.Windows.Forms.Label exist_label in exist_labelList)
            {
                this.room_groupBox.Controls.Remove(exist_label);
            }
            room_comboBox.Items.Clear();
            room_comboBox.Text = "選擇需要新增檢查的房間";
            for (int i = 0; i < select_roomList.Count; i++)
            {
                Element room_elem = select_roomList[i];
                string room_name = room_elem.Name;
                if (i == 0)
                {
                    Label room_label_s = new Label();
                    room_label_s.Name = "room_label_s" + i.ToString();
                    room_label_s.Text = room_name;
                    room_label_s.BorderStyle = BorderStyle.FixedSingle;
                    room_label_s.Location = new System.Drawing.Point(selected_label.Location.X, selected_label.Location.Y + 50);
                    room_label_s.Font = selected_label.Font;
                    room_label_s.Parent = room_groupBox;
                    room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                    room_label_s.Padding = new Padding(3);
                    room_label_s.Cursor = Cursors.Hand;
                    room_label_s.Width = 150;
                    room_label_s.Height = 30;
                    room_label_s.Click += create_label_Click;
                    this.room_groupBox.Controls.Add(room_label_s);
                }
                else
                {
                    if (i % 3 == 0)
                    {
                        System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i - 3).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                        Label room_label_s = new Label();
                        room_label_s.Name = "room_label_s" + i.ToString();
                        room_label_s.Text = room_name;
                        room_label_s.BorderStyle = BorderStyle.FixedSingle;
                        room_label_s.Location = new System.Drawing.Point(previous_label.Location.X, previous_label.Location.Y + 40);
                        room_label_s.Font = selected_label.Font;
                        room_label_s.Parent = room_groupBox;
                        room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                        room_label_s.Padding = new Padding(3);
                        room_label_s.Cursor = Cursors.Hand;
                        room_label_s.Width = 150;
                        room_label_s.Height = 30;
                        room_label_s.Click += create_label_Click;
                        this.room_groupBox.Controls.Add(room_label_s);
                    }
                    else
                    {
                        System.Windows.Forms.Label previous_label = this.room_groupBox.Controls.Find("room_label_s" + (i - 1).ToString(), true).OfType<System.Windows.Forms.Label>().SingleOrDefault();
                        Label room_label_s = new Label();
                        room_label_s.Name = "room_label_s" + i.ToString();
                        room_label_s.Text = room_name;
                        room_label_s.BorderStyle = BorderStyle.FixedSingle;
                        room_label_s.Location = new System.Drawing.Point(previous_label.Location.X + previous_label.Width + 20, previous_label.Location.Y);
                        room_label_s.Font = selected_label.Font;
                        room_label_s.Parent = room_groupBox;
                        room_label_s.TextAlign = ContentAlignment.MiddleCenter;
                        room_label_s.Padding = new Padding(3);
                        room_label_s.Cursor = Cursors.Hand;
                        room_label_s.Width = 150;
                        room_label_s.Height = 30;
                        room_label_s.Click += create_label_Click;
                        this.room_groupBox.Controls.Add(room_label_s);
                    }
                }
            }
            for (int i = 0; i < other_roomList.Count; i++)
            {
                Element un_room_elem = other_roomList[i];
                room_comboBox.Items.Add(un_room_elem.Name);
            }
        }

        private void dimension_listView_DoubleClick(object sender, EventArgs e)
        {
            List<ElementId> element_IDList = new List<ElementId>();
            string element_id = dimension_listView.SelectedItems[0].SubItems[0].Text;
            int elem_id = Convert.ToInt32(element_id);
            ElementId s_elem_id = new ElementId(elem_id);
            element_IDList.Add(s_elem_id);
            m_connect.RevitDoc.ShowElements(element_IDList);
            m_connect.RevitDoc.Selection.SetElementIds(element_IDList);
        }

        private void selection_button_Click(object sender, EventArgs e)
        {
            room_checkBox.Enabled = true;
            dimension_checkBox.Enabled = true;
            check_button.Enabled = true;
            dimension_listView.Visible = false;
            description_label.Visible = false;
            outcome_label.Visible = false;
            selection_button.Visible = false;
            if (room_checkBox.Checked == true)
            {
                room_groupBox.Visible = true;
            }
        }

        int time_s = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time_s = time_s - 1;
            if (time_s == 0)
            {
                timer1.Stop();
                this.Dispose();
                LoginForm login = new LoginForm(m_connect, m_CheckDimension_ExternalEvent);
                login.Show();
                //this.Close();
            }
            this.Text = "通道寬度檢核 - 認證失敗，" + time_s.ToString() + "秒後強制關閉";
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
    }
}
