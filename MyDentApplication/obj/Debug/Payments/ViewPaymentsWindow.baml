   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      *    $MyDentApplication.ViewPaymentsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ 	Ver pagos��5      $	��703��5      $	��496��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6Z    � 	Resources 5	      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5
      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      �� -   5         ��Name$ lblTotal��5   
   $�� ��6   $��
10,0,10,10q�6'   $��Right=�6;   $��29��6^   $��Bottom=�6j   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpStartDate��5      $
��Left=�6$   $��
109,43,0,0q�6?   $	��Top=�6S   $��212.64��6k     5      -   5        $ 	dpEndDate��5      $
��Left=�6"   $��
109,73,0,0q�6=   $	��Top=�6Q   $��212.64��6i   �� 5      $��Fecha de inicio:��6
   $
��Left=�6%   $��24��6@   $��	11,43,0,0q�6L   $	��Top=�6_   $��93��6w   �� 5      $��Fecha de fin:��6
   $
��Left=�6"   $��24��6=   $��26.333,72,0,0q�6I   $	��Top=�6`   $��77.667��6x   �� 5      -   5        $ 
lblPatient��5   
   $��	Paciente:��6   $
��Left=�62   $��27��6M   $��42.419,12,0,0q�6Y   $	��Top=�6p   $��61.581��6�   �� 5      -   5         ��Name$ 
cbPatients��5      $��26��6!   $��
109,12,0,0q�6-   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��212.64��6�   �� 5      -   5        	 ��Name$	 	btnFilter��5      $��Filtrar��6   $��346.14,41.48,0,0q�60   
 � Cursor$

 Hand��6J   $��7��6X   $
��Left=�6e   $
��68.5��6�   $��29��6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5      -	   5          Name$ 
dgPayments��5      $��10,105.75,10,44q�6!      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tipo de pago��62      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.19* 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *Type+6H     5      $ Banco��67   $
 0.1* 6F    6   �� *	Bank.Name+6F     5      $ Cantidad��6K   $ 0.17* 6]    6   �� *Amount+ �� StringFormat$ ${0:n}��6]     5      $ No. de voucher o cheque��6@   $
 0.3* 6a    6   �� *VoucherCheckNumber+6a     5      $ Fecha de pago��6   $ 0.19* 60    5      `� 5        `� 	Converter# [  6   .1�5!   	   ��  �� Path$ PaymentDate�6   5"   	   5$        5%      $ Observaciones��69   $ 0.19* 6P    6   �� *Observation+6P     5&      $ Paciente��6   $ 0.25* 6+    5'      `� 5(      $ (Exp. No. {0}) {1} {2}��6   .1�5)      �� $% PaymentFolio.Patient.AssignedId�6&   �� 5*   	   $$ PaymentFolio.Patient.FirstName�6   �� 5+   	   $# PaymentFolio.Patient.LastName�6   5,   	   5.      50      51      52      