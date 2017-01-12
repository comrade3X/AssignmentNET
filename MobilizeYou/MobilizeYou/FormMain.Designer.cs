using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MobilizeYou
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listCustomersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.membershipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.employeeToolStripMenuItem,
            this.orderToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(709, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listProductToolStripMenuItem});
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.productToolStripMenuItem.Text = "Product";
            // 
            // listProductToolStripMenuItem
            // 
            this.listProductToolStripMenuItem.Name = "listProductToolStripMenuItem";
            this.listProductToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.listProductToolStripMenuItem.Text = "List Product";
            this.listProductToolStripMenuItem.Click += new System.EventHandler(this.listProductToolStripMenuItem_Click);
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listCustomersToolStripMenuItem});
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.customersToolStripMenuItem.Text = "Customers";
            // 
            // listCustomersToolStripMenuItem
            // 
            this.listCustomersToolStripMenuItem.Name = "listCustomersToolStripMenuItem";
            this.listCustomersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listCustomersToolStripMenuItem.Text = "List Customers";
            this.listCustomersToolStripMenuItem.Click += new System.EventHandler(this.listCustomersToolStripMenuItem_Click);
            // 
            // employeeToolStripMenuItem
            // 
            this.employeeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listEmployeeToolStripMenuItem,
            this.membershipToolStripMenuItem});
            this.employeeToolStripMenuItem.Name = "employeeToolStripMenuItem";
            this.employeeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.employeeToolStripMenuItem.Text = "Employee";
            // 
            // listEmployeeToolStripMenuItem
            // 
            this.listEmployeeToolStripMenuItem.Name = "listEmployeeToolStripMenuItem";
            this.listEmployeeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.listEmployeeToolStripMenuItem.Text = "List Employee";
            this.listEmployeeToolStripMenuItem.Click += new System.EventHandler(this.listEmployeeToolStripMenuItem_Click);
            // 
            // membershipToolStripMenuItem
            // 
            this.membershipToolStripMenuItem.Name = "membershipToolStripMenuItem";
            this.membershipToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.membershipToolStripMenuItem.Text = "Membership";
            this.membershipToolStripMenuItem.Click += new System.EventHandler(this.membershipToolStripMenuItem_Click);
            // 
            // orderToolStripMenuItem
            // 
            this.orderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOrderToolStripMenuItem,
            this.listOrdersToolStripMenuItem});
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            this.orderToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.orderToolStripMenuItem.Text = "Orders";
            // 
            // createOrderToolStripMenuItem
            // 
            this.createOrderToolStripMenuItem.Name = "createOrderToolStripMenuItem";
            this.createOrderToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.createOrderToolStripMenuItem.Text = "Create Order";
            this.createOrderToolStripMenuItem.Click += new System.EventHandler(this.createOrderToolStripMenuItem_Click);
            // 
            // listOrdersToolStripMenuItem
            // 
            this.listOrdersToolStripMenuItem.Name = "listOrdersToolStripMenuItem";
            this.listOrdersToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.listOrdersToolStripMenuItem.Text = "List Orders";
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportToolStripMenuItem1});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportToolStripMenuItem.Text = "Reports";
            // 
            // reportToolStripMenuItem1
            // 
            this.reportToolStripMenuItem1.Name = "reportToolStripMenuItem1";
            this.reportToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.reportToolStripMenuItem1.Text = "List Report";
            this.reportToolStripMenuItem1.Click += new System.EventHandler(this.reportToolStripMenuItem1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 426);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Mobilize You";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem customersToolStripMenuItem;
        private ToolStripMenuItem listCustomersToolStripMenuItem;
        private ToolStripMenuItem productToolStripMenuItem;
        private ToolStripMenuItem listProductToolStripMenuItem;
        private ToolStripMenuItem employeeToolStripMenuItem;
        private ToolStripMenuItem listEmployeeToolStripMenuItem;
        private ToolStripMenuItem membershipToolStripMenuItem;
        private ToolStripMenuItem orderToolStripMenuItem;
        private ToolStripMenuItem createOrderToolStripMenuItem;
        private ToolStripMenuItem listOrdersToolStripMenuItem;
        private ToolStripMenuItem reportToolStripMenuItem;
        private ToolStripMenuItem reportToolStripMenuItem1;
    }
}

