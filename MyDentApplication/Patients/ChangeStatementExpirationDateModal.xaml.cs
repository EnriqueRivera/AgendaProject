using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ChangeStatementExpirationDateModal.xaml
	/// </summary>
	public partial class ChangeStatementExpirationDateModal : Window
    {
        #region Instance variables
        private Model.Statement _statement;
        #endregion

        #region Constructors
        public ChangeStatementExpirationDateModal(Model.Statement statement)
        {
            this.InitializeComponent();

            _statement = statement;

            dtpCreationDate.SelectedDate = _statement.CreationDate;
            dtpExpirationDate.SelectedDate = _statement.ExpirationDate;
        }
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (dtpExpirationDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dtpExpirationDate.SelectedDate.Value.Date < _statement.CreationDate.Date)
            {
                MessageBox.Show("La fecha de expiración no puede ser menor a la fecha de creación", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _statement.ExpirationDate = dtpExpirationDate.SelectedDate.Value;

                if (Controllers.BusinessController.Instance.Update<Model.Statement>(_statement))
                {
                    this.Close();    
                }
                else
                {
                    MessageBox.Show("Error al tratar de cambiar la fecha de expiración del estado de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
        }
        #endregion
    }
}