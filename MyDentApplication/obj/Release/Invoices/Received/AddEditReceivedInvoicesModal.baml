   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication4   .MyDentApplication.AddEditReceivedInvoicesModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$ Agregar factura��$	��390��$	��361�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblProvider��$��
Proveedor:��$
��Left=�$��27��$��77.667,13,0,0q�$	��Top=�$��70.333���� -   $ lblInvoiceDate��$��Fecha de facturación:��$
��Left=�$��27��$��23.667,77,0,0q�$	��Top=�$��124.333���� -    ��Name$ btnAddUpdateInvoice��$��Agregar��$
��Left=�$��29��$��100.67,280.697,0,0q�$	��Top=�$��91�� � Cursor$
 Hand��$��9���� -   $ 	btnCancel��$��Cancelar��$
��Left=�$��29��$��209.67,280.697,0,0q�$	��Top=�$��91��$
 Hand��$��10���� -   $ lblFolio��$��Folio:��$
��Left=�$��27��$��105.667,141,0,0q�$	��Top=�$��42.333���� -   $ lblTotalAmount��$��Total:��$
��Left=�$��27��$��107.333,205,0,0q�$	��Top=�$��40.667���� -	   	 ��Name$	 cbProviders��$��26��$��
153,13,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��194.5��(  "System.Windows.Controls.DatePicker  -
   
  Name$
 dtpInvoiceDate��$
��Left=�$��27��$��
153,77,0,0q�$	��Top=�$��194.5��$��3��{�.  �� -   $ lbPurchaseDate��$��Fecha de compra:��$
��Left=�$��27��$��41.667,109,0,0q�$	��Top=�$��106.333��  -   $
 dtpPurchaseDate��$
��Left=�$��27��$��153,110,0,0q�$	��Top=�$��194.5��$��4���� -    ��Name$ txtFolio��$
��Left=�$��27��$��153,142,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��194.5��$��5��$��1q�
���   ��� -   $ txtTotalAmount��$
��Left=�$��27��$��153,206,0,0q�$
 Wrap=�$	��Top=�$��194.5��$��7��$��1q�
���   ��� -   $ lblPaidMethod��$��Método de pago:��$
��Left=�$��27��$��41.667,173,0,0q�$	��Top=�$��106.333���� -   $	 cbPaidMethod��$��26��$��153,173,0,0q�$	��Top=�$��Normal"�$��6��$
��Left=�$��194.5��.@���Efectivo����
Cheque����Transferencia bancaria����Tarjeta crédito����Tarjeta débito����Especie���� -   $ lblIsInvoiced��$��
Facturado:��$
��Left=�$��27��$��81.042,45,0,0q�$	��Top=�$��66.958���� -    ��Name$ chkIsInvoiced��$�� ��$
��Left=�$��18��$��153,51.332,0,0q�$	��Top=�$��15.333��$��2���� -   $ 	lblIsPaid��$��Pagado:��$
��Left=�$��27��$��92.042,237,0,0q�$	��Top=�$��55.958���� -   $ 	chkIsPaid��$�� ��$
��Left=�$��18��$��153,243,0,0q�$	��Top=�$��15.333��$��8��