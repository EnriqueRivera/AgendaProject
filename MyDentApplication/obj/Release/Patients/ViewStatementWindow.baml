   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  +    %MyDentApplication.ViewStatementWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Estado de cuenta��$	��640��$��586.5�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+��� $��4��$
��Auto=�.���  � RowDefinitions �� $	��73* ��� $��8* � � ColumnDefinitions �� $
��317* ��� $
��315* �.+��� -   	 ��Name$	 
lblPatient��$��	Paciente:��$
��Left=�$��27��$��10,9,0,0q�$	��Top=�$��61.581��$
��Bold"��� -   $	 lblPatient_Copy��$��Registro de tratamientos��$
��Left=�$��27��$��10,61.924,0,0q�$	��Top=�$��152.228��$
��Bold"��� -   $	 lblPatient_Copy1��$��Registro de pagos��$
��Left=�$��27��$��10,298.882,0,0q�$	��Top=�$��118.728��$
��Bold"��� -   $	 lblTreatmentsCount��$��No. de tratamientos:��$��27��$��10,61.924,10,0q�$	��Top=�$��Right=�$��1���� -   $	 lblPaymentsCount��$��No. de pagos:��$��27��$��10,298.882,10,0q�$	��Top=�$��Right=�$��1���� -   $	 lblPatient_Copy4��$��Resumen��$
��Left=�$��27��$��10,535.89,0,0q�$	��Top=�$��118.728��$
��Bold"��� -	   $	 lblPatient_Copy5��$"��Monto total de tratamientos:��$
��Left=�$��27��$��10,567.89,0,0q�$	��Top=�$��162.081���� -
   $	 lblPatient_Copy6��$��Monto total de pagos:��$
��Left=�$��27��$��42.515,599.89,0,0q�$	��Top=�$��129.566���� -   $	 lblPatient_Copy7��$��Pendiente por pagar:��$
��Left=�$��27��$��49.603,631.89,0,0q�$	��Top=�$��122.478���� -   
 ��Name$
 lblTotalAmountTreatments��$��20��$��177.081,572.89,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   ��� -   $
 lblTotalAmountPayments��$��20��$��177.081,604.89,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   ��� -   $
 lblGrandTotal��$��20��$��177.081,636.89,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   ��� -    ��Name$ btnGeneratePdf��$��Generar PDF��$��0,0,79,10.083q� � Cursor$
 Hand��$��6��$��Right=�$��95��$��29��$��Bottom=�$��1��$��1���� -   $ 	btnCancel��$��Cerrar��$��	0,0,10,10q�$
 Hand��$��7��$��Right=�$��64��$��29��$��Bottom=�$��1��$��1���� -   $	 lblAccountStatusNumber��$�� ��$��27��$��0,8,10,0q�$	��Top=�$��Right=�$��83.253��$��1���� -   $ btnSendMail��$��Enviar correo��$��0,0,179,10.083q�$
 Hand��$��6��$��Right=�$��95.104��$��29��$��Bottom=�$��1��$��1���� -   $#	 lblAccountStatusNumberCaption��$��Estado de cuenta No.��$��27��$��73,8,98.253,0q�$	��Top=�$��Right=�$
��Bold"�$��1���� -   $	 	lblStatus��$��Enviando correo...��$��Right=�$��0,0,14,42.082q�$��123.168��
���  ��$
��Bold"�$��Italic$�$��13.333(�$��29��$��Bottom=�$��1��$��1��$y�Hidden=��� -   $	 lblPatientName��$�� ��$��27��$��76.581,8,263.527,0q�$	��Top=�$��2��&   System.Windows.Controls.DataGrid  -     Name$ dgTreatments��$��10,88.924,10,0q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1��$��2��$��191.791��$	��Top=����� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Tratamiento��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.4* 1  +System.Windows.Controls.DataGridBoundColumn   Binding `�  �� StringFormat$ {0} - {1} ({2})��.1���  �� Path$! TreatmentPrice.TreatmentKey��� $ TreatmentPrice.Name��� $ TreatmentPrice.Type�  $ Cantidad��$
 0.2*  �� *
Quantity+  $ Precio��$
 0.2*  �� *Price+$ ${0:n}��  $ 	Descuento��$ 0.25*  `� $
 {0}%��.1��� $ Discount�  $ Total��$ 0.25*  �� *Total+$ ${0:n}��  $ Fecha��$ 0.22*  `�  `� 	Converter# [  .1��� $ TreatmentDate�  -   $ 
dgPayments��$��10,325.882,10,0q� .  .  .  $ Single=� .   .  $��1��$��2��$��191.791��$	��Top=����� *ObservableData+   $ Tipo de pago��$
 0.2*  �� *Type+  $ Banco��$ 0.18*  �� *	Bank.Name+  $ Cantidad��$ 0.18*  �� *Amount+$ ${0:n}��  $ No. de voucher o cheque��$ 0.35*  �� *VoucherCheckNumber+  $ Fecha de pago��$ 0.22*  `� # [  .1��� $ PaymentDate�  $ Observaciones��$ 0.25*  �� *Observation+�� -   $	 lblPatient_Copy2��$��*Saldo a favor:��$
��Left=�$��27��$��83.603,663.89,0,0q�$	��Top=�$��88.478���� -   $	 lblPositiveBalanceMessage��$����*Saldo a favor generado al liquidar el estado de 
cuenta, lo cual indica que ya pudo ser utilizado 
en alguna otra transacción.��$
��Left=�$��10,0,0,10.083q�$	��297��$��Italic$�$��60.417��$��Bottom=�$��1���� -   $
 lblPositiveBalance��$��20��$��177.081,668.89,10,0q�$	��Top=�$��Normal"�$��Italic$�
���   ��� 
���   �$��1q�$��Right=�$	��160��$��0,562.89,14,0q�$	��Top=�$	��160��$��1��._��� -    ��Name$ imgPatientPicture��$��	0,0,-1,-1q��� -   $	 lblPatient_Copy8��$��Foto del paciente��$��Right=�$��27��$��0,535.89,31.436,0q�$	��Top=�$��118.728��$
��Bold"�$��1��