   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  *    $MyDentApplication.ViewPaymentsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ 	Ver pagos��$	��703��$	��496�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblTotal��$�� ��$��
10,0,10,10q�$��Right=�$��29��$��Bottom=�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$
��Left=�$��
109,43,0,0q�$	��Top=�$��212.64��  -   $ 	dpEndDate��$
��Left=�$��
109,73,0,0q�$	��Top=�$��212.64���� $��Fecha de inicio:��$
��Left=�$��24��$��	11,43,0,0q�$	��Top=�$��93���� $��Fecha de fin:��$
��Left=�$��24��$��26.333,72,0,0q�$	��Top=�$��77.667���� -   $ 
lblPatient��$��	Paciente:��$
��Left=�$��27��$��42.419,12,0,0q�$	��Top=�$��61.581���� -    ��Name$ 
cbPatients��$��26��$��
109,12,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��212.64���� -   	 ��Name$	 	btnFilter��$��Filtrar��$��346.14,41.48,0,0q�
 � Cursor$

 Hand��$��7��$
��Left=�$
��68.5��$��29��$	��Top=�&   System.Windows.Controls.DataGrid  -	     Name$ 
dgPayments��$��10,105.75,10,44q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Tipo de pago��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.19* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Type+  $ Banco��$
 0.1*  �� *	Bank.Name+  $ Cantidad��$ 0.17*  �� *Amount+ �� StringFormat$ ${0:n}��  $ No. de voucher o cheque��$
 0.3*  �� *VoucherCheckNumber+  $ Fecha de pago��$ 0.19*  `�  `� 	Converter# [  .1���  �� Path$ PaymentDate�  $ Observaciones��$ 0.19*  �� *Observation+  $ Paciente��$ 0.25*  `� $ (Exp. No. {0}) {1} {2}��.1��� $% PaymentFolio.Patient.AssignedId��� $$ PaymentFolio.Patient.FirstName��� $# PaymentFolio.Patient.LastName�