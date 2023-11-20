using System.Windows.Forms;

namespace DeweyDecimal_Latest.Forms
{
    public partial class FindingCallNumberGame : Form
    {
        public FindingCallNumberGame()
        {
            InitializeComponent();
            HandleWindowState();
            this.ControlBox = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Removes sizing options
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //