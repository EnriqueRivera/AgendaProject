   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication0   *MyDentApplication.AddEditGeneralPaidsModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$ Agregar pago general��$	��390��$	��294�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�.��� -    �Name$ 
LayoutRoot��$��	0,0,-4,-4q�.+��� -    ��Name$ lblProviderName��$��Nombre del proveedor:��$
��Left=�$��27��$��13.5,31,0,0q�$	��Top=�$��134.5���� -    ��Name$ btnAddUpdateGeneralPaid��$��Agregar��$
��Left=�$��29��$��86.67,205.697,0,0q�$	��Top=�$��91�� � Cursor$
 Hand��$��6���� -   $ 	btnCancel��$��Cancelar��$
��Left=�$��29��$��195.67,205.697,0,0q�$	��Top=�$��91��$
 Hand��$��7���� -   $ lblFolio��$��Número de ticket:��$
��Left=�$��27��$��41.667,95,0,0q�$	��Top=�$��106.333���� -   $ lblTotalAmount��$��Total:��$
��Left=�$��27��$��107.333,159,0,0q�$	��Top=�$��40.667���� -   $ lbPurchaseDate��$��Fecha de compra:��$
��Left=�$��27��$��41.667,63,0,0q�$	��Top=�$��106.333��(  "System.Windows.Controls.DatePicker  -	     Name$ dtpPurchaseDate��$
��Left=�$��27��$��
153,64,0,0q�$	��Top=�$��194.5��$��2���� -
   	 ��Name$	 txtTicketNumber��$
��Left=�$��27��$��
153,96,0,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$��194.5��$��3��$��1q�
���   ��� -   $	 txtTotalAmount��$
��Left=�$��27��$��153,160,0,0q�$

 Wrap=�$	��Top=�$��194.5��$��5��$��1q�
���   ��� -   $ lblPaidMethod��$��Método de pago:��$
��Left=�$��27��$��41.667,127,0,0q�$	��Top=�$��106.333���� -    ��Name$ cbPaidMethod��$��26��$��153,127,0,0q�$	��Top=�$��Normal"�$��4��$
��Left=�$��194.5��.@���Efectivo����
Cheque����Transferencia bancaria����Tarjeta crédito����Tarjeta débito����Especie���� -   $	 txtProviderName��$
��Left=�$��27��$��
153,31,0,0q�$

 Wrap=�$	��Top=�$��194.5��$��1��$��1q�
���   �