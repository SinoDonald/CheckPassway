namespace CheckPassway
{
    partial class PasswayWidth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswayWidth));
            this.check_button = new System.Windows.Forms.Button();
            this.option_label = new System.Windows.Forms.Label();
            this.option_panel = new System.Windows.Forms.Panel();
            this.room_checkBox = new System.Windows.Forms.CheckBox();
            this.dimension_checkBox = new System.Windows.Forms.CheckBox();
            this.room_groupBox = new System.Windows.Forms.GroupBox();
            this.function_label = new System.Windows.Forms.Label();
            this.selected_label = new System.Windows.Forms.Label();
            this.room_comboBox = new System.Windows.Forms.ComboBox();
            this.room_selection_label = new System.Windows.Forms.Label();
            this.passway_label = new System.Windows.Forms.Label();
            this.passway_width_textBox = new System.Windows.Forms.TextBox();
            this.unit_label = new System.Windows.Forms.Label();
            this.outcome_label = new System.Windows.Forms.Label();
            this.description_label = new System.Windows.Forms.Label();
            this.dimension_listView = new System.Windows.Forms.ListView();
            this.selection_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.option_panel.SuspendLayout();
            this.room_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // check_button
            // 
            this.check_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.check_button.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.check_button.FlatAppearance.BorderSize = 2;
            this.check_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.check_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.check_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_button.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.check_button.Location = new System.Drawing.Point(300, 16);
            this.check_button.Name = "check_button";
            this.check_button.Size = new System.Drawing.Size(106, 37);
            this.check_button.TabIndex = 2;
            this.check_button.Text = "檢查";
            this.check_button.UseVisualStyleBackColor = true;
            this.check_button.Click += new System.EventHandler(this.check_button_Click);
            // 
            // option_label
            // 
            this.option_label.AutoSize = true;
            this.option_label.BackColor = System.Drawing.SystemColors.Info;
            this.option_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.option_label.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.option_label.Location = new System.Drawing.Point(28, 67);
            this.option_label.Margin = new System.Windows.Forms.Padding(5);
            this.option_label.Name = "option_label";
            this.option_label.Padding = new System.Windows.Forms.Padding(3);
            this.option_label.Size = new System.Drawing.Size(94, 32);
            this.option_label.TabIndex = 3;
            this.option_label.Text = "檢查項目";
            this.option_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // option_panel
            // 
            this.option_panel.Controls.Add(this.room_checkBox);
            this.option_panel.Controls.Add(this.dimension_checkBox);
            this.option_panel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.option_panel.Location = new System.Drawing.Point(135, 68);
            this.option_panel.Name = "option_panel";
            this.option_panel.Size = new System.Drawing.Size(287, 32);
            this.option_panel.TabIndex = 4;
            // 
            // room_checkBox
            // 
            this.room_checkBox.AutoSize = true;
            this.room_checkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.room_checkBox.Location = new System.Drawing.Point(165, 5);
            this.room_checkBox.Name = "room_checkBox";
            this.room_checkBox.Size = new System.Drawing.Size(93, 25);
            this.room_checkBox.TabIndex = 1;
            this.room_checkBox.Text = "通道房間";
            this.room_checkBox.UseVisualStyleBackColor = true;
            this.room_checkBox.CheckedChanged += new System.EventHandler(this.room_checkBox_CheckedChanged);
            // 
            // dimension_checkBox
            // 
            this.dimension_checkBox.AutoSize = true;
            this.dimension_checkBox.Checked = true;
            this.dimension_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dimension_checkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dimension_checkBox.Location = new System.Drawing.Point(3, 5);
            this.dimension_checkBox.Name = "dimension_checkBox";
            this.dimension_checkBox.Size = new System.Drawing.Size(125, 25);
            this.dimension_checkBox.TabIndex = 0;
            this.dimension_checkBox.Text = "通道尺寸標註";
            this.dimension_checkBox.UseVisualStyleBackColor = true;
            // 
            // room_groupBox
            // 
            this.room_groupBox.AutoSize = true;
            this.room_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.room_groupBox.Controls.Add(this.function_label);
            this.room_groupBox.Controls.Add(this.selected_label);
            this.room_groupBox.Controls.Add(this.room_comboBox);
            this.room_groupBox.Controls.Add(this.room_selection_label);
            this.room_groupBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.room_groupBox.Location = new System.Drawing.Point(28, 120);
            this.room_groupBox.Name = "room_groupBox";
            this.room_groupBox.Padding = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.room_groupBox.Size = new System.Drawing.Size(394, 153);
            this.room_groupBox.TabIndex = 5;
            this.room_groupBox.TabStop = false;
            this.room_groupBox.Text = "房間選擇";
            // 
            // function_label
            // 
            this.function_label.AutoSize = true;
            this.function_label.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.function_label.ForeColor = System.Drawing.Color.DarkRed;
            this.function_label.Location = new System.Drawing.Point(132, 101);
            this.function_label.Name = "function_label";
            this.function_label.Size = new System.Drawing.Size(118, 17);
            this.function_label.TabIndex = 3;
            this.function_label.Text = "*點擊一下移除選擇";
            this.function_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selected_label
            // 
            this.selected_label.AutoSize = true;
            this.selected_label.BackColor = System.Drawing.Color.OldLace;
            this.selected_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selected_label.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.selected_label.Location = new System.Drawing.Point(12, 95);
            this.selected_label.Name = "selected_label";
            this.selected_label.Padding = new System.Windows.Forms.Padding(3);
            this.selected_label.Size = new System.Drawing.Size(114, 29);
            this.selected_label.TabIndex = 2;
            this.selected_label.Text = "已選擇的房間";
            this.selected_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // room_comboBox
            // 
            this.room_comboBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.room_comboBox.FormattingEnabled = true;
            this.room_comboBox.Location = new System.Drawing.Point(115, 45);
            this.room_comboBox.Name = "room_comboBox";
            this.room_comboBox.Size = new System.Drawing.Size(264, 29);
            this.room_comboBox.TabIndex = 1;
            this.room_comboBox.Text = "選擇需要新增檢查的房間";
            this.room_comboBox.SelectedIndexChanged += new System.EventHandler(this.room_comboBox_SelectedIndexChanged);
            // 
            // room_selection_label
            // 
            this.room_selection_label.AutoSize = true;
            this.room_selection_label.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.room_selection_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.room_selection_label.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.room_selection_label.Location = new System.Drawing.Point(12, 42);
            this.room_selection_label.Name = "room_selection_label";
            this.room_selection_label.Padding = new System.Windows.Forms.Padding(3);
            this.room_selection_label.Size = new System.Drawing.Size(82, 29);
            this.room_selection_label.TabIndex = 0;
            this.room_selection_label.Text = "房間名稱";
            this.room_selection_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passway_label
            // 
            this.passway_label.AutoSize = true;
            this.passway_label.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.passway_label.Location = new System.Drawing.Point(26, 19);
            this.passway_label.Name = "passway_label";
            this.passway_label.Size = new System.Drawing.Size(101, 24);
            this.passway_label.TabIndex = 6;
            this.passway_label.Text = "通道寬度 : ";
            this.passway_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passway_width_textBox
            // 
            this.passway_width_textBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.passway_width_textBox.Location = new System.Drawing.Point(131, 20);
            this.passway_width_textBox.Name = "passway_width_textBox";
            this.passway_width_textBox.Size = new System.Drawing.Size(118, 29);
            this.passway_width_textBox.TabIndex = 7;
            this.passway_width_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passway_width_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.accept_number_KeyPress);
            // 
            // unit_label
            // 
            this.unit_label.AutoSize = true;
            this.unit_label.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.unit_label.Location = new System.Drawing.Point(261, 19);
            this.unit_label.Name = "unit_label";
            this.unit_label.Size = new System.Drawing.Size(28, 24);
            this.unit_label.TabIndex = 8;
            this.unit_label.Text = "m";
            this.unit_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outcome_label
            // 
            this.outcome_label.AutoSize = true;
            this.outcome_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outcome_label.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.outcome_label.Location = new System.Drawing.Point(28, 287);
            this.outcome_label.Name = "outcome_label";
            this.outcome_label.Size = new System.Drawing.Size(164, 26);
            this.outcome_label.TabIndex = 9;
            this.outcome_label.Text = "通過通道寬度檢核";
            this.outcome_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // description_label
            // 
            this.description_label.AutoSize = true;
            this.description_label.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.description_label.ForeColor = System.Drawing.Color.DarkRed;
            this.description_label.Location = new System.Drawing.Point(25, 294);
            this.description_label.Name = "description_label";
            this.description_label.Size = new System.Drawing.Size(206, 16);
            this.description_label.TabIndex = 10;
            this.description_label.Text = "*點擊該欄位兩下，即可觀看標註位置";
            this.description_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dimension_listView
            // 
            this.dimension_listView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dimension_listView.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dimension_listView.FullRowSelect = true;
            this.dimension_listView.GridLines = true;
            this.dimension_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dimension_listView.HideSelection = false;
            this.dimension_listView.Location = new System.Drawing.Point(28, 316);
            this.dimension_listView.Name = "dimension_listView";
            this.dimension_listView.Size = new System.Drawing.Size(394, 183);
            this.dimension_listView.TabIndex = 11;
            this.dimension_listView.UseCompatibleStateImageBehavior = false;
            this.dimension_listView.View = System.Windows.Forms.View.Details;
            this.dimension_listView.DoubleClick += new System.EventHandler(this.dimension_listView_DoubleClick);
            // 
            // selection_button
            // 
            this.selection_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selection_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.selection_button.FlatAppearance.BorderSize = 2;
            this.selection_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.selection_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed;
            this.selection_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selection_button.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.selection_button.Location = new System.Drawing.Point(307, 279);
            this.selection_button.Name = "selection_button";
            this.selection_button.Size = new System.Drawing.Size(108, 31);
            this.selection_button.TabIndex = 12;
            this.selection_button.Text = "重新選擇";
            this.selection_button.UseVisualStyleBackColor = true;
            this.selection_button.Click += new System.EventHandler(this.selection_button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PasswayWidth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.selection_button);
            this.Controls.Add(this.dimension_listView);
            this.Controls.Add(this.description_label);
            this.Controls.Add(this.outcome_label);
            this.Controls.Add(this.unit_label);
            this.Controls.Add(this.passway_width_textBox);
            this.Controls.Add(this.passway_label);
            this.Controls.Add(this.room_groupBox);
            this.Controls.Add(this.option_panel);
            this.Controls.Add(this.option_label);
            this.Controls.Add(this.check_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PasswayWidth";
            this.Padding = new System.Windows.Forms.Padding(5, 5, 20, 25);
            this.Text = "通道寬度檢核";
            this.Load += new System.EventHandler(this.PasswayWidth_Load);
            this.option_panel.ResumeLayout(false);
            this.option_panel.PerformLayout();
            this.room_groupBox.ResumeLayout(false);
            this.room_groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button check_button;
        private System.Windows.Forms.Label option_label;
        private System.Windows.Forms.Panel option_panel;
        private System.Windows.Forms.CheckBox room_checkBox;
        private System.Windows.Forms.CheckBox dimension_checkBox;
        private System.Windows.Forms.GroupBox room_groupBox;
        private System.Windows.Forms.Label selected_label;
        private System.Windows.Forms.ComboBox room_comboBox;
        private System.Windows.Forms.Label room_selection_label;
        private System.Windows.Forms.Label function_label;
        private System.Windows.Forms.Label passway_label;
        private System.Windows.Forms.TextBox passway_width_textBox;
        private System.Windows.Forms.Label unit_label;
        private System.Windows.Forms.Label outcome_label;
        private System.Windows.Forms.Label description_label;
        private System.Windows.Forms.ListView dimension_listView;
        private System.Windows.Forms.Button selection_button;
        private System.Windows.Forms.Timer timer1;
    }
}