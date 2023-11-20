/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

// ---------------- Usings ---------------- //

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// ---------------- End Usings ---------------- //

namespace DeweyDecimal_Latest
{
    internal class PanelHelper
    {

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Removes all panels
        /// </summary>
        /// <param name="panel"></param>
        private void ClearColumnPanels(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Panel)
                {
                    // Using Recursively to clear nested panels
                    ClearColumnPanels((Panel)control);
                }
                else
                {
                    panel.Controls.Remove(control);
                    control.Dispose(); // Dispose of the label control
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Clears labels from panels
        /// </summary>
        /// <param name="columns"></param>
        public void ClearLabelsFromColumnPanels(List<Panel> columns)
        {
            foreach (var column in columns)
            {
                foreach (Control control in column.Controls)
                {
                    if (control is Label)
                    {
                        column.Controls.Remove(control);
                        control.Dispose();
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Attatches a mouse click event listener to a list of panels
        /// </summary>
        /// <param name="panels"></param>
        /// <param name="handler"></param>
        public void AttachMouseClickEventHandlers(List<Panel> panels, MouseEventHandler handler)
        {
            foreach (var panel in panels)
            {
                if (panel != null)
                {
                    panel.MouseClick += handler;
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Sets the background color of the selected question
        /// </summary>
        /// <param name="selectedQuestion"></param>
        public void SetQuestionBackground(Panel selectedQuestion)
        {
            selectedQuestion.BackColor = ColorTranslator.FromHtml("#3fa9ff");
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Creates a "Ready to select" effect for answer panels
        /// </summary>
        /// <param name="secondColumnPanels"></param>
        public void ReadyToSelectEffect(List<Panel> secondColumnPanels)
        {
            foreach (var panel in secondColumnPanels)
            {
                if (panel.Enabled)
                {
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.BackColor = Color.FromArgb(100, 0, 0, 0);

                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Settings panels to enabled
        /// </summary>
        public void EnablePanels(List<Panel> firstColumnPanels, List<Panel> secondColumnPanels)
        {
            foreach (var panel in firstColumnPanels)
            {
                panel.Enabled = true;
            }

            foreach (var panel in secondColumnPanels)
            {
                panel.Enabled = true;
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets the answer panel's colors
        /// </summary>
        /// <param name="secondColumnPanels"></param>
        public void ResetAnswerPanelColor(List<Panel> secondColumnPanels)
        {
            foreach (var panel in secondColumnPanels)
            {
                panel.BackColor = ColorTranslator.FromHtml("#ff943f");
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Clears selection effect after an answer is selected
        /// </summary>
        public void ClearGlowEffectFromAnswers(List<Panel> secondColumnPanels)
        {
            foreach (var panel in secondColumnPanels)
            {
                // Clear the glow effect
                panel.BackColor = panel.BackColor = ColorTranslator.FromHtml("#ff943f");
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets quesiton panel's color
        /// </summary>
        public void ResetQuestionPanelColor(List<Panel> firstColumnPanels)
        {
            foreach (var panel in firstColumnPanels)
            {
                // Resetting color using HEX Value
                panel.BackColor = ColorTranslator.FromHtml("#3fa9ff");
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Creates labels for panel
        /// </summary>
        /// <param name="items"></param>
        /// <param name="columns"></param>
        public void CreateLabelsForColumn(List<string> items, List<Panel> columns, List<Label> panelLabels)
        {
            var tool = new ToolTip();
            for (int i = 0; i < items.Count && i < columns.Count; i++)
            {
                var label = new Label
                {
                    Text = items[i],
                    ForeColor = Color.White,
                    UseMnemonic = false,
                    Enabled = false,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                };

                tool.SetToolTip(columns[i], items[i]);


                panelLabels.Add(label);

                columns[i].Tag = label.Text;
                columns[i].Controls.Add(label);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///  Dynamically add tooltip to a control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="text"></param>
        public void CreateToolTip(Control control, string text)
        {
            var tool = new ToolTip();
            tool.SetToolTip(control, text);
        }
    }
}// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
