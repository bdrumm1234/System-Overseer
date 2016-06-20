namespace System_Overseer
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.helpPanel = new System.Windows.Forms.Panel();
            this.helpLabelA = new System.Windows.Forms.Label();
            this.helpPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpPanel
            // 
            this.helpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPanel.Controls.Add(this.helpLabelA);
            this.helpPanel.Location = new System.Drawing.Point(6, 6);
            this.helpPanel.Name = "helpPanel";
            this.helpPanel.Size = new System.Drawing.Size(272, 246);
            this.helpPanel.TabIndex = 0;
            // 
            // helpLabelA
            // 
            this.helpLabelA.Location = new System.Drawing.Point(5, 5);
            this.helpLabelA.Name = "helpLabelA";
            this.helpLabelA.Size = new System.Drawing.Size(262, 237);
            this.helpLabelA.TabIndex = 0;
            this.helpLabelA.Text = resources.GetString("helpLabelA.Text");
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 259);
            this.Controls.Add(this.helpPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::System_Overseer.Properties.Resources.favicon;
            this.Name = "Help";
            this.Text = "System Overseer - Help";
            this.helpPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel helpPanel;
        private System.Windows.Forms.Label helpLabelA;
    }
}