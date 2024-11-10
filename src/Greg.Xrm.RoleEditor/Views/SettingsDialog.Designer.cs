namespace Greg.Xrm.RoleEditor.Views
{
	partial class SettingsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tableLayout2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabUX = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.chkHide2 = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chkHide1 = new System.Windows.Forms.CheckBox();
			this.chkUseLegacyIcons = new System.Windows.Forms.CheckBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnResetTableGrouping = new System.Windows.Forms.Button();
			this.txtTableGrouping = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.btnResetMiscGrouping = new System.Windows.Forms.Button();
			this.txtMiscGrouping = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.notificationPanel1 = new Greg.Xrm.Core.Views.NotificationPanel();
			this.tableLayout.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlFooter.SuspendLayout();
			this.tableLayout2.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabUX.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 1;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayout.Controls.Add(this.pnlFooter, 0, 3);
			this.tableLayout.Controls.Add(this.tableLayout2, 0, 2);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 4;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayout.Size = new System.Drawing.Size(784, 561);
			this.tableLayout.TabIndex = 0;
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.White;
			this.pnlHeader.Controls.Add(this.label2);
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(784, 60);
			this.pnlHeader.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(346, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Configure here your preferences about the tool look && feel and behavior.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "_n.RoleEditor Settings";
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.btnCancel);
			this.pnlFooter.Controls.Add(this.btnOk);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFooter.Location = new System.Drawing.Point(0, 501);
			this.pnlFooter.Margin = new System.Windows.Forms.Padding(0);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(784, 60);
			this.pnlFooter.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(600, 15);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 30);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(690, 15);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 30);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// tableLayout2
			// 
			this.tableLayout2.ColumnCount = 1;
			this.tableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout2.Controls.Add(this.tabs, 0, 1);
			this.tableLayout2.Controls.Add(this.notificationPanel1, 0, 0);
			this.tableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout2.Location = new System.Drawing.Point(0, 60);
			this.tableLayout2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout2.Name = "tableLayout2";
			this.tableLayout2.RowCount = 2;
			this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout2.Size = new System.Drawing.Size(784, 441);
			this.tableLayout2.TabIndex = 2;
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabUX);
			this.tabs.Controls.Add(this.tabPage1);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(3, 3);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(778, 435);
			this.tabs.TabIndex = 5;
			// 
			// tabUX
			// 
			this.tabUX.AutoScroll = true;
			this.tabUX.Controls.Add(this.label6);
			this.tabUX.Controls.Add(this.label7);
			this.tabUX.Controls.Add(this.chkHide2);
			this.tabUX.Controls.Add(this.label5);
			this.tabUX.Controls.Add(this.label4);
			this.tabUX.Controls.Add(this.chkHide1);
			this.tabUX.Controls.Add(this.chkUseLegacyIcons);
			this.tabUX.Location = new System.Drawing.Point(4, 22);
			this.tabUX.Name = "tabUX";
			this.tabUX.Padding = new System.Windows.Forms.Padding(10);
			this.tabUX.Size = new System.Drawing.Size(770, 409);
			this.tabUX.TabIndex = 1;
			this.tabUX.Text = "Layout preferences";
			this.tabUX.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(14, 148);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(272, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Configure the preferences for the role editor panel.";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(15, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Role Editor";
			// 
			// chkHide2
			// 
			this.chkHide2.AutoSize = true;
			this.chkHide2.Location = new System.Drawing.Point(16, 83);
			this.chkHide2.Name = "chkHide2";
			this.chkHide2.Size = new System.Drawing.Size(120, 17);
			this.chkHide2.TabIndex = 4;
			this.chkHide2.Text = "Hide managed roles";
			this.chkHide2.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(525, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Configure which roles are visible by default. It requires to close and reopen the" +
    " plugin to take effect.";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(14, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Role Browser";
			// 
			// chkHide1
			// 
			this.chkHide1.AutoSize = true;
			this.chkHide1.Location = new System.Drawing.Point(16, 60);
			this.chkHide1.Name = "chkHide1";
			this.chkHide1.Size = new System.Drawing.Size(155, 17);
			this.chkHide1.TabIndex = 1;
			this.chkHide1.Text = "Hide not customizable roles";
			this.chkHide1.UseVisualStyleBackColor = true;
			// 
			// chkUseLegacyIcons
			// 
			this.chkUseLegacyIcons.AutoSize = true;
			this.chkUseLegacyIcons.Location = new System.Drawing.Point(16, 174);
			this.chkUseLegacyIcons.Name = "chkUseLegacyIcons";
			this.chkUseLegacyIcons.Size = new System.Drawing.Size(149, 17);
			this.chkUseLegacyIcons.TabIndex = 0;
			this.chkUseLegacyIcons.Text = "Use legacy privilege icons";
			this.chkUseLegacyIcons.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.AutoScroll = true;
			this.tabPage1.Controls.Add(this.btnResetTableGrouping);
			this.tabPage1.Controls.Add(this.txtTableGrouping);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.btnResetMiscGrouping);
			this.tabPage1.Controls.Add(this.txtMiscGrouping);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(10);
			this.tabPage1.Size = new System.Drawing.Size(770, 409);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Privilege grouping";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnResetTableGrouping
			// 
			this.btnResetTableGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetTableGrouping.Location = new System.Drawing.Point(643, 451);
			this.btnResetTableGrouping.Name = "btnResetTableGrouping";
			this.btnResetTableGrouping.Size = new System.Drawing.Size(80, 30);
			this.btnResetTableGrouping.TabIndex = 5;
			this.btnResetTableGrouping.Text = "Reset";
			this.btnResetTableGrouping.UseVisualStyleBackColor = true;
			// 
			// txtTableGrouping
			// 
			this.txtTableGrouping.AcceptsTab = true;
			this.txtTableGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTableGrouping.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTableGrouping.Location = new System.Drawing.Point(13, 288);
			this.txtTableGrouping.Multiline = true;
			this.txtTableGrouping.Name = "txtTableGrouping";
			this.txtTableGrouping.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTableGrouping.Size = new System.Drawing.Size(710, 157);
			this.txtTableGrouping.TabIndex = 4;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoEllipsis = true;
			this.label8.Location = new System.Drawing.Point(13, 251);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(710, 34);
			this.label8.TabIndex = 3;
			this.label8.Text = "The JSON dictionary below allows to configure the structure of the table privileg" +
    "es list.\r\nAny table not indicated in the configuration below will be put in a ge" +
    "neric \"General\" group:\r\n";
			// 
			// btnResetMiscGrouping
			// 
			this.btnResetMiscGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetMiscGrouping.Location = new System.Drawing.Point(643, 210);
			this.btnResetMiscGrouping.Name = "btnResetMiscGrouping";
			this.btnResetMiscGrouping.Size = new System.Drawing.Size(80, 30);
			this.btnResetMiscGrouping.TabIndex = 2;
			this.btnResetMiscGrouping.Text = "Reset";
			this.btnResetMiscGrouping.UseVisualStyleBackColor = true;
			// 
			// txtMiscGrouping
			// 
			this.txtMiscGrouping.AcceptsTab = true;
			this.txtMiscGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMiscGrouping.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMiscGrouping.Location = new System.Drawing.Point(13, 47);
			this.txtMiscGrouping.Multiline = true;
			this.txtMiscGrouping.Name = "txtMiscGrouping";
			this.txtMiscGrouping.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMiscGrouping.Size = new System.Drawing.Size(710, 157);
			this.txtMiscGrouping.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoEllipsis = true;
			this.label3.Location = new System.Drawing.Point(13, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(710, 34);
			this.label3.TabIndex = 0;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// notificationPanel1
			// 
			this.notificationPanel1.AutoSize = true;
			this.notificationPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notificationPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notificationPanel1.Location = new System.Drawing.Point(0, 0);
			this.notificationPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.notificationPanel1.Name = "notificationPanel1";
			this.notificationPanel1.Size = new System.Drawing.Size(784, 1);
			this.notificationPanel1.TabIndex = 6;
			// 
			// SettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.tableLayout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.Text = "_n.RoleEditor Settings";
			this.tableLayout.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlFooter.ResumeLayout(false);
			this.tableLayout2.ResumeLayout(false);
			this.tableLayout2.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.tabUX.ResumeLayout(false);
			this.tabUX.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayout;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Panel pnlFooter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayout2;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabUX;
		private System.Windows.Forms.CheckBox chkUseLegacyIcons;
		private System.Windows.Forms.TabPage tabPage1;
		private Core.Views.NotificationPanel notificationPanel1;
		private System.Windows.Forms.TextBox txtMiscGrouping;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnResetMiscGrouping;
		private System.Windows.Forms.CheckBox chkHide1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkHide2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnResetTableGrouping;
		private System.Windows.Forms.TextBox txtTableGrouping;
		private System.Windows.Forms.Label label8;
	}
}