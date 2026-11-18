using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.Messaging;
using System;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
    public partial class FetchXmlFilterDialog : Form
    {
        private readonly string entityName;

        public FetchXmlFilterDialog(IMessenger messenger, string entityName, string fetchXml)
        {
            InitializeComponent();

            this.RegisterHelp(messenger, Topics.FetchXmlFilterDialog);

            this.entityName = entityName;
            this.FetchXml = fetchXml;
            this.Text = $"FetchXML filter - {entityName}";

            var text = string.IsNullOrWhiteSpace(fetchXml) ? FetchXmlFilter.CreateTemplate(entityName) : fetchXml;
            this.txtFetchXml.Text = NormalizeNewLines(text);
        }

        /// <summary>
        /// The FetchXML defined by the user, or null if no filter should be applied.
        /// </summary>
        public string FetchXml { get; private set; }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.txtFetchXml.Select(0, 0);
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
            this.txtFetchXml.Text = string.Empty;
        }

        private void OnFormatClick(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
            if (string.IsNullOrWhiteSpace(this.txtFetchXml.Text)) return;

            this.txtFetchXml.Text = NormalizeNewLines(FetchXmlFilter.Format(this.txtFetchXml.Text));
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            this.errorProvider.Clear();

            var fetchXml = this.txtFetchXml.Text.Trim();
            if (string.IsNullOrWhiteSpace(fetchXml))
            {
                this.FetchXml = null;
                this.DialogResult = DialogResult.OK;
                Close();
                return;
            }

            var errors = FetchXmlFilter.Validate(fetchXml, this.entityName);
            if (errors.Count > 0)
            {
                this.errorProvider.SetError(this.txtFetchXml, string.Join(Environment.NewLine, errors));
                return;
            }

            this.FetchXml = fetchXml;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private static string NormalizeNewLines(string text)
        {
            return (text ?? string.Empty).Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }
    }
}
