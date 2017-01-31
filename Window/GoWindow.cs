using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoWin {
    public partial class GoWindow : Form {
        public DrawPanel panelDraw;

        public GoWindow() {
            InitializeComponent();

            panelDraw = new DrawPanel();
            panelDraw.Dock = DockStyle.Fill;
            panelLayer.Controls.Add(panelDraw);
        }
    }

    public class DrawPanel : Panel {
        public DrawPanel() {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint  |
                ControlStyles.UserPaint             |
                ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
