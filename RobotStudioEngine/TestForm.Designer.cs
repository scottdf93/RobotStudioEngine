namespace RobotStudioEngine
{
    partial class TestForm
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
            this.GetAllControllersButton = new System.Windows.Forms.Button();
            this.GetControllerButton = new System.Windows.Forms.Button();
            this.ControllerListView = new BrightIdeasSoftware.FastObjectListView();
            this.ControllerNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ControllerIpAddressColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.SystemIdColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.MacAddressColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.NetscanIdColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.IsVirtualColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.IpAddressTextBox = new System.Windows.Forms.TextBox();
            this.MacAddressTextBox = new System.Windows.Forms.TextBox();
            this.GuidTextBox = new System.Windows.Forms.TextBox();
            this.ControllerNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerListView)).BeginInit();
            this.SuspendLayout();
            // 
            // GetAllControllersButton
            // 
            this.GetAllControllersButton.Location = new System.Drawing.Point(12, 115);
            this.GetAllControllersButton.Name = "GetAllControllersButton";
            this.GetAllControllersButton.Size = new System.Drawing.Size(111, 23);
            this.GetAllControllersButton.TabIndex = 0;
            this.GetAllControllersButton.Text = "Get All Controllers";
            this.GetAllControllersButton.UseVisualStyleBackColor = true;
            this.GetAllControllersButton.Click += new System.EventHandler(this.GetAllControllersButton_Click);
            // 
            // GetControllerButton
            // 
            this.GetControllerButton.Location = new System.Drawing.Point(12, 144);
            this.GetControllerButton.Name = "GetControllerButton";
            this.GetControllerButton.Size = new System.Drawing.Size(111, 23);
            this.GetControllerButton.TabIndex = 1;
            this.GetControllerButton.Text = "Get Controller(s)";
            this.GetControllerButton.UseVisualStyleBackColor = true;
            this.GetControllerButton.Click += new System.EventHandler(this.GetControllerButton_Click);
            // 
            // ControllerListView
            // 
            this.ControllerListView.AllColumns.Add(this.ControllerNameColumn);
            this.ControllerListView.AllColumns.Add(this.ControllerIpAddressColumn);
            this.ControllerListView.AllColumns.Add(this.SystemIdColumn);
            this.ControllerListView.AllColumns.Add(this.MacAddressColumn);
            this.ControllerListView.AllColumns.Add(this.NetscanIdColumn);
            this.ControllerListView.AllColumns.Add(this.IsVirtualColumn);
            this.ControllerListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControllerListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.ControllerListView.CellEditUseWholeCell = false;
            this.ControllerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ControllerNameColumn,
            this.ControllerIpAddressColumn,
            this.SystemIdColumn,
            this.MacAddressColumn,
            this.NetscanIdColumn,
            this.IsVirtualColumn});
            this.ControllerListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ControllerListView.HideSelection = false;
            this.ControllerListView.Location = new System.Drawing.Point(12, 12);
            this.ControllerListView.Name = "ControllerListView";
            this.ControllerListView.ShowGroups = false;
            this.ControllerListView.Size = new System.Drawing.Size(726, 97);
            this.ControllerListView.TabIndex = 5;
            this.ControllerListView.UseCompatibleStateImageBehavior = false;
            this.ControllerListView.View = System.Windows.Forms.View.Details;
            this.ControllerListView.VirtualMode = true;
            // 
            // ControllerNameColumn
            // 
            this.ControllerNameColumn.AspectName = "SystemName";
            this.ControllerNameColumn.Text = "Controller Name";
            this.ControllerNameColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ControllerNameColumn.Width = 120;
            // 
            // ControllerIpAddressColumn
            // 
            this.ControllerIpAddressColumn.AspectName = "IPAddress";
            this.ControllerIpAddressColumn.Text = "IP Address";
            this.ControllerIpAddressColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ControllerIpAddressColumn.Width = 120;
            // 
            // SystemIdColumn
            // 
            this.SystemIdColumn.AspectName = "SystemId";
            this.SystemIdColumn.Text = "System ID";
            this.SystemIdColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SystemIdColumn.Width = 120;
            // 
            // MacAddressColumn
            // 
            this.MacAddressColumn.AspectName = "MacAddress";
            this.MacAddressColumn.Text = "MAC";
            this.MacAddressColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MacAddressColumn.Width = 120;
            // 
            // NetscanIdColumn
            // 
            this.NetscanIdColumn.AspectName = "NetscanId";
            this.NetscanIdColumn.Text = "Net Scan ID";
            this.NetscanIdColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NetscanIdColumn.Width = 120;
            // 
            // IsVirtualColumn
            // 
            this.IsVirtualColumn.AspectName = "IsVirtual";
            this.IsVirtualColumn.CheckBoxes = true;
            this.IsVirtualColumn.Text = "Is Virtual";
            this.IsVirtualColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IsVirtualColumn.Width = 120;
            // 
            // IpAddressTextBox
            // 
            this.IpAddressTextBox.Location = new System.Drawing.Point(130, 145);
            this.IpAddressTextBox.Name = "IpAddressTextBox";
            this.IpAddressTextBox.Size = new System.Drawing.Size(135, 20);
            this.IpAddressTextBox.TabIndex = 6;
            this.IpAddressTextBox.Text = "127.0.0.1";
            this.IpAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MacAddressTextBox
            // 
            this.MacAddressTextBox.Location = new System.Drawing.Point(271, 145);
            this.MacAddressTextBox.Name = "MacAddressTextBox";
            this.MacAddressTextBox.Size = new System.Drawing.Size(135, 20);
            this.MacAddressTextBox.TabIndex = 7;
            this.MacAddressTextBox.Text = "00-00-00-00-00-00";
            this.MacAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GuidTextBox
            // 
            this.GuidTextBox.Location = new System.Drawing.Point(412, 145);
            this.GuidTextBox.Name = "GuidTextBox";
            this.GuidTextBox.Size = new System.Drawing.Size(135, 20);
            this.GuidTextBox.TabIndex = 8;
            this.GuidTextBox.Text = "000000";
            this.GuidTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ControllerNameTextBox
            // 
            this.ControllerNameTextBox.Location = new System.Drawing.Point(553, 145);
            this.ControllerNameTextBox.Name = "ControllerNameTextBox";
            this.ControllerNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.ControllerNameTextBox.TabIndex = 9;
            this.ControllerNameTextBox.Text = "ControllerName";
            this.ControllerNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "MAC Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(462, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "GUID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(603, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Name";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(750, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ControllerNameTextBox);
            this.Controls.Add(this.GuidTextBox);
            this.Controls.Add(this.MacAddressTextBox);
            this.Controls.Add(this.IpAddressTextBox);
            this.Controls.Add(this.ControllerListView);
            this.Controls.Add(this.GetControllerButton);
            this.Controls.Add(this.GetAllControllersButton);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Form";
            ((System.ComponentModel.ISupportInitialize)(this.ControllerListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetAllControllersButton;
        private System.Windows.Forms.Button GetControllerButton;
        private BrightIdeasSoftware.FastObjectListView ControllerListView;
        private BrightIdeasSoftware.OLVColumn ControllerNameColumn;
        private BrightIdeasSoftware.OLVColumn ControllerIpAddressColumn;
        private BrightIdeasSoftware.OLVColumn IsVirtualColumn;
        private BrightIdeasSoftware.OLVColumn SystemIdColumn;
        private BrightIdeasSoftware.OLVColumn MacAddressColumn;
        private BrightIdeasSoftware.OLVColumn NetscanIdColumn;
        private System.Windows.Forms.TextBox IpAddressTextBox;
        private System.Windows.Forms.TextBox MacAddressTextBox;
        private System.Windows.Forms.TextBox GuidTextBox;
        private System.Windows.Forms.TextBox ControllerNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

