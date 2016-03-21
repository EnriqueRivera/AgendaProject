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
	/// Interaction logic for AddEditInstrumentRevisionModal.xaml
	/// </summary>
	public partial class AddEditInstrumentRevisionModal : Window
	{
        #region Instance variables
        private Model.InventoryAvailability _selectedInstrument;
        private bool _isUpdateComment;
        #endregion

        #region Constructors
        public AddEditInstrumentRevisionModal(Model.InventoryAvailability selectedInstrument)
		{
			this.InitializeComponent();

            _selectedInstrument = selectedInstrument;

            _isUpdateComment = selectedInstrument.InstrumentCommentId != null;

            if (_isUpdateComment)
            {
                txtComment.Text = _selectedInstrument.Comment;
            }

            txtQuantity.Text = _selectedInstrument.Quantity.ToString();

            if (_selectedInstrument.UsedOn == 0)
            {
                lblUses.Visibility = System.Windows.Visibility.Hidden;
                lblMaxUses.Visibility = System.Windows.Visibility.Hidden;
                txtUsesLeft.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                txtUsesLeft.Text = _selectedInstrument.UsesLeft.ToString();
                lblMaxUses.ToolTip = lblMaxUses.Content = "(Max. " + _selectedInstrument.MaxUses + ")";
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddEditRevision_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string quantityText = txtQuantity.Text.Trim();
            string usesLeftText = txtUsesLeft.Text.Trim();
            int quantity = 0;
            int usesLeft = 0;

            if (AreValidFields(quantityText, usesLeftText, out quantity, out usesLeft) == false)
            {
                return;
            }

            if (_isUpdateComment)
            {
                Model.InstrumentComment commentToUpdate = Controllers.BusinessController.Instance.FindById<Model.InstrumentComment>(_selectedInstrument.InstrumentCommentId);

                if (commentToUpdate != null)
                {
                    commentToUpdate.Comment = txtComment.Text.Trim();
                    commentToUpdate.CommentDate = DateTime.Now;
                    commentToUpdate.InstrumentId = _selectedInstrument.InstrumentId;

                    if (UpdateComment(commentToUpdate) == false)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                Model.InstrumentComment commentToAdd = new Model.InstrumentComment()
                {
                    Comment = txtComment.Text.Trim(),
                    CommentDate = DateTime.Now,
                    InstrumentId = _selectedInstrument.InstrumentId
                };

                if (AddComment(commentToAdd) == false)
                {
                    return;
                }
            }

            //Update quantities
            Model.Instrument instrumentToUpdate = Controllers.BusinessController.Instance.FindById<Model.Instrument>(_selectedInstrument.InstrumentId);
            if (instrumentToUpdate != null)
            {
                instrumentToUpdate.Quantity = quantity;
                instrumentToUpdate.UsesLeft = usesLeft;

                if (UpdateTreatment(instrumentToUpdate) == false)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("No se pudo " + (_isUpdateComment ? "actualizar" : "agregar") + " el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private bool UpdateTreatment(Model.Instrument instrumentToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Instrument>(instrumentToUpdate))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se pudo " + (_isUpdateComment ? "actualizar" : "agregar") + " el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool AddComment(Model.InstrumentComment commentToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.InstrumentComment>(commentToAdd))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se pudo agregar el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool UpdateComment(Model.InstrumentComment commentToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.InstrumentComment>(commentToUpdate))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool AreValidFields(string quantityText, string usesLeftText, out int quantity, out int usesLeft)
        {
            quantity = -1;
            usesLeft = -1;

            if (int.TryParse(quantityText, out quantity) == false || quantity < 0)
            {
                MessageBox.Show("Cantidad inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_selectedInstrument.UsedOn > 0)
            {
                if (int.TryParse(usesLeftText, out usesLeft) == false)
                {
                    MessageBox.Show("Usos restantes inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (usesLeft > _selectedInstrument.MaxUses)
                {
                    MessageBox.Show("El máxmio número de usos para este instrumento es de " + _selectedInstrument.MaxUses, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (quantity == 0)
                {
                    usesLeft = 0;
                }
                else if (quantity != 1)
                {
                    MessageBox.Show("La cantidad debe ser 0 o 1 para los instrumentos utilizados en un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}