   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  *    $MyDentApplication.CashRegisterWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window�� � Title$
 Caja��$	��910��$��653.5�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.��� -    �Name$ 
LayoutRoot��.+��� $��4��$
��Auto=�.���  � ColumnDefinitions �� $
��184* ��� $
��267* � � RowDefinitions �� $	��41* ��� $��4* �.+��� -    ��Name$ btnSave��$��Guardar��$��133.728,0,0,10q�	 � Cursor$
	 Hand��$��6��$
��Left=�$��91��$��29��$��Bottom=�$��1���� -   $ 	btnCancel��$��Cerrar��$��	0,0,10,10q�$
	 Hand��$��7��$��Right=�$��91��$��29��$��Bottom=�$��1��$��1���� -   
 ��Name$
 gbPacientInfo��$��Información del paciente��$��10,538.57,10,0q�$
��Bold"�$��2q�
���   �
���   �$��238.11��$	��Top=�.���� -    ��Name$ svPacientInfo��$��	0,0,-2,-2q�$
��Auto=�.��� -   $ gridPacientInfo��$��Center=�$��216.114��.+��� -    ��Name$ lblExpNoCaption��$��Exp. No:��$��35.612,-1,0,0q�$��Normal"�$��25��$	��Top=�$
��Left=�$��60.522��$��13.333(��� -	    ��Name$ lblExpNo��$��20��$��101.134,4,0,0q�$	��Top=�$��Normal"�$��Italic$�$��13.333(��� -
   $ lblCellPhoneCaption��$��Teléfono celular:��$��
10,74,10,0q�$��Normal"�$��25��$	��Top=�$��13.333(��� -   $ lblCellPhone��$�� ��$��20��$��
10,99,10,0q�$	��Top=�$��Normal"� �� TextAlignment$ Center=�$��Italic$�$��13.333(��� -   $ lblHomePhoneCaption��$��Teléfono de casa:��$��10,121,10,0q�$��Normal"�$��25��$	��Top=�$��13.333(��� -   $ lblHomePhone��$�� ��$��20��$��10,146,10,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$�$��13.333(��� -   $ lblEmailCaption��$��Email:��$��10,168,10,0q�$��Normal"�$��25��$	��Top=�$��13.333(��� -   $ lblEmail��$��20��$��10,193,10,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$�$��13.333(��� -   $ lblPacientNameCaption��$��Nombre completo:��$��10,26.001,10,0q�$��Normal"�$��28��$	��Top=�$��13.333(��� -   $ lblPacientName��$�� ��$��20��$��
10,52,10,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$�$��13.333(��� -   $ btnClearForm��$��Limpiar caja��$��
0,0,106,10q�$
	 Hand��$��7��$��Right=�$��91��$��1��$��29��$��Bottom=�$��1���� -   $ 	lblStatus��$��Enviando correo...��$
��Left=�$��	10,0,0,42q�$��123.168��
���  ��$
��Bold"�$��Italic$�$��13.333(�$��29��$��Bottom=�$��1��$y�Hidden=��� $��Totales��$��443.785��$��10,89.785,10,0q�$	��Top=�$
��Bold"�$��14.667(�
���   �$��2q�.��� $��0,0,-2,-3.507q�.+��� -   $ lblPatient_Copy5��$"��Monto total de tratamientos:��$��39��$��10,16.417,10,0q�$	��Top=�$��21.333(�$
��Bold"�$��Italic$��� -   $ lblPatient_Copy6��$��Monto total de pagos:��$��39��$��10,116.467,10,0q�$	��Top=�$��21.333(�$
��Bold"��� -   $ lblPatient_Copy7��$��Pendiente por pagar:��$��39��$��10,221.375,10,0q�$	��Top=�$��21.333(�$
��Bold"�
���  ���� -   $ lblPatient_Copy8��$��Saldo a favor:��$��34��$��10,324.375,10,0q�$	��Top=�$��21.333(�$
��Bold"����	#FF27B027�� -   $ lblTotalAmountTreatments��$
��27.5��$��10,57.417,10,0q�$	��Top=�$
��Bold"�$��Italic$�
���   �$��18.667(�$ Right=��� $�$���� $�0���� -   $ lblTotalAmountPayments��$
��27.5��$��10,156.967,10,0q�$	��Top=�$
��Bold"�$��Italic$�
���   �$��18.667(�$ Right=��� $�$���� $�0���� -   $ lblGrandTotal��$
��27.5��$��10,260.375,10,0q�$	��Top=�$
��Bold"�$��Italic$�
���  ��$��18.667(�$��$0��$ Right=��� -   $ lblPositiveBalance��$
��27.5��$��10,358.375,10,0q�$	��Top=�$
��Bold"�$��Italic$����	#FF27B027$��18.667(�$��$0��$ Right=��� $��Registro de tratamientos��$��266.285��$��10,89.785,20.064,0q�$	��Top=�$
��Bold"�$��14.667(�
���   �$��2q�$��1��.��� $��0,0,-2,-3.507q�.+��� -   $ svTreatments��$��10,10,65.231,0q�$
��Auto=�$��1q�$	��190��$	��Top=�
���   �$��13.333(�.���� 
���   �$��1q�._��� -    ��Name$ spTreatments���� -   $ btnAddTreatment��$
	 Hand��$��0,84.32,10,0q�$��50��$	��Top=� � ToolTip$ Agregar tratamiento...��$��Right=�$��49.265��.���� $6��0/MyDentApplication;component/Images/add_icon.png���� -   $ lblTreatmentsCount��$��No. de tratamientos:��$��27��$��10,205,65.231,0q�$	��Top=�$��Right=�$��Normal"��� $��Paciente��$��74.785��$��10,10,405.436,0q�$	��Top=�$
��Bold"�$��14.667(�
���   �$��2q�$��2��.���� -     ��Name$ 
cbPatients��$��26��$��10,10,10.145,0q�$	��Top=�$��Normal"�$��1��$��14.667(��� $��Registro de pagos��$��266.285��$��10,361.07,20.064,0q�$	��Top=�$
��Bold"�$��14.667(�
���   �$��2q�$��1��.��� $��0,0,-2,-3.507q�.+��� -!   $ 
svPayments��$��10,9.278,67.103,0q�$
��Auto=�$��0q�$	��190��$	��Top=�$��13.333(�.���� 
���   �$��1q�._��� -"   $ 
spPayments���� -#   $ btnAddCreditCardPayment��$��49.265��$	��Top=�$ Pago con tarjeta...��
 � Tag$ Tarjeta��$��0,77.5,10,0q�$��50��$��Right=�$
	 Hand��.���� $>��8/MyDentApplication;component/Images/credit_card_icon.png���� -$   $ btnAddCheckPayment��$��49.265��$	��Top=�$ Pago con cheque...��$ Cheque��$��0,132.5,10,0q�$��50��$��Right=�$
	 Hand��.���� $8��2/MyDentApplication;component/Images/check_icon.png���� -%   $ btnAddCashPayment��$��49.265��$	��Top=�$ Pago en efectivo...��$ Efectivo��$��0,22.5,10,0q�$��50��$��Right=�$
	 Hand��.���� $7��1/MyDentApplication;component/Images/cash_icon.png���� -&   $ lblPaymentsCount��$��No. de pagos:��$��29.007��$��14,204.278,67.103,0q�$	��Top=�$��Right=�$��Normal"��� -'   $
 gbAccountStatusNumber��$��Estado de cuenta No.��$��74.785��$��0,10,20.064,0q�$	��Top=�$
��Bold"�$��14.667(�
���   �$��2q�$��1��$��Right=�$��373.436��$y�Hidden=�.���� -(   $ lblAccountStatusNumber��$�� ��$��27��$��10,10,8.5,0q�$	��Top=�$��Right=��� $��Folio��$��10,632.355,20.064,0q�$
��Bold"�$��14.667(�
���   �$��2q�$��1��$��144.325��$	��Top=�.��� $��0,0,-2,-3.507q�.+��� -)   $ lblPatient_Copy2��$��No. de folio generado:��$
��29.5��$��10,25.598,0,0q�$	��Top=�$
��Bold"�$��14.667(�$
��Left=�$��165.395���� -*   $ lblPaymentFolioNumber��$��20��$��180.395,30.598,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   �$��14.667(��� -+   $( "lblGeneratedStatementNumberCaption��$3��-No. del estado de cuenta generado/modificado:��$
��30.5��$��10,60.098,143.501,0q�$	��Top=�$
��Bold"�$��14.667(�$y�Hidden=��� -,   $! lblGeneratedStatementNumber��$��20��$��355.435,65.098,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   �$��14.667(��� --   $ btnPrintMail��$��Enviar correo��$��16.936,0,0,10q�$
	 Hand��$��6��$
��Left=�$��111.792��$��29��$��Bottom=�$��1��{�.  