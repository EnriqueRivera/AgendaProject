   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication4   .MyDentApplication.AddEditOutgoingInvoicesModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$" Agregar recibo de honorarios��$	��390��$��338.5�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��$��	0,0,-2,-2q�.+��� -    ��Name$ 
lblPatient��$��	Paciente:��$
��Left=�$��27��$��87.667,31,0,0q�$	��Top=�$��60.333���� -   $ lblInvoiceDate��$��Fecha de facturación:��$
��Left=�$��27��$��23.667,95,0,0q�$	��Top=�$��124.333���� -    ��Name$ btnAddUpdateInvoice��$��Agregar��$
��Left=�$��29��$��87.667,0,0,10q�$��Bottom=�$��91�� � Cursor$
 Hand��$��8���� -   $ 	btnCancel��$��Cancelar��$
��Left=�$��29��$��196.667,0,0,10q�$��Bottom=�$��91��$
 Hand��$��9���� -   $ lblFolio��$��Folio:��$
��Left=�$��27��$��105.667,159,0,0q�$	��Top=�$��42.333���� -   $ lblTotalAmount��$��Total:��$
��Left=�$��27��$��107.333,223,0,0q�$	��Top=�$��40.667���� -	   	 ��Name$	 
cbPatients��$��26��$��
153,31,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��194.5��(  "System.Windows.Controls.DatePicker  -
   
  Name$
 dtpInvoiceDate��$
��Left=�$��27��$��
153,95,0,0q�$	��Top=�$��194.5��$��3��{�.  �� -   $ lbPurchaseDate��$��Fecha del pago:��$
��Left=�$��27��$��51.667,127,0,0q�$	��Top=�$��96.333��  -   $
 dtpPaidDate��$
��Left=�$��27��$��153,128,0,0q�$	��Top=�$��194.5��$��4���� -    ��Name$ txtFolio��$
��Left=�$��27��$��153,160,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��194.5��$��5��$��1q�
���   ��� -   $ txtTotalAmount��$
��Left=�$��27��$��153,224,0,0q�$
 Wrap=�$	��Top=�$��194.5��$��7��$��1q�
���   ��� -   $ lblPaidMethod��$��Método de pago:��$
��Left=�$��27��$��41.667,191,0,0q�$	��Top=�$��106.333���� -   $	 cbPaidMethod��$��26��$��153,191,0,0q�$	��Top=�$��Normal"�$��6��$
��Left=�$��194.5��.@���Efectivo����
Cheque����Transferencia bancaria����Tarjeta crédito����Tarjeta débito����Especie���� -   $ lblIsInvoiced��$��
Facturado:��$
��Left=�$��27��$��81.042,63,0,0q�$	��Top=�$��66.958���� -    ��Name$ chkIsInvoiced��$�� ��$
��Left=�$��18��$��153,69.332,0,0q�$	��Top=�$��15.333��$��2��