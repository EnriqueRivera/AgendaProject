   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      4    .MyDentApplication.ViewPaymentFolioDetailWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Detalle del folio��5      $	��640��5      $��586.5��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��66    � WindowStartupLocation$ CenterScreen=�6u    � 	Resources 5	      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5
      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      �� $��4��6   $
��Auto=�6"   .��5      �  � RowDefinitions 5      �� 5      $	��79* �6   �� 5      $��9* �6    � ColumnDefinitions 5      �� 5      $
��317* �6   �� 5      $
��315* �6   .+�5      �� -   5         ��Name$ 
lblPatient��5      $��	Paciente:��6    $
��Left=�64   $��27��6O   $��10,9,0,0q�6[   $	��Top=�6m   $��61.581��6�   $
��Bold"�6�   �� 5      -   5        $ lblPatient_Copy��5      $��Registro de tratamientos��6%   $
��Left=�6H   $��27��6c   $��10,61.924,0,0q�6o   $	��Top=�6�   $��152.228��6�   $
��Bold"�6�   �� 5      -   5        $ lblPatient_Copy1��5      $��Registro de pagos��6&   $
��Left=�6B   $��27��6]   $��10,298.882,0,0q�6i   $	��Top=�6�   $��118.728��6�   $
��Bold"�6�   �� 5      -   5        $ lblTreatmentsCount��5      $��No. de tratamientos:��6(   $��27��6G   $��18.527,61.924,10,0q�6S   $	��Top=�6o   $��Right=�6�   $��1��6�   �� 5      -   5        $ lblPaymentsCount��5      $��No. de pagos:��6&   $��27��6>   $��18.527,298.882,10,0q�6J   $	��Top=�6g   $��Right=�6   $��1��6�   �� 5      -   5        $ lblPatient_Copy4��5      $��Resumen��6&   $
��Left=�68   $��27��6S   $��10,535.89,0,0q�6_   $	��Top=�6v   $��118.728��6�   $
��Bold"�6�   �� 5      -	   5        $ lblPatient_Copy5��5      $"��Monto total de tratamientos:��6&   $
��Left=�6M   $��27��6h   $��10,567.89,0,0q�6t   $	��Top=�6�   $��162.081��6�   �� 5      -
   5        $ lblPatient_Copy6��5      $��Monto total de pagos:��6&   $
��Left=�6F   $��27��6a   $��42.515,599.89,0,0q�6m   $	��Top=�6�   $��129.566��6�   �� 5      -   5        	 ��Name$	 lblTotalAmountTreatments��5      $��20��62   $��177.081,572.89,10,0q�6>   $	��Top=�6[   $��Normal"�6s   $��Italic$�6�   
���   �6�   �� 5       -   5        $	 lblTotalAmountPayments��5       $��20��60   $��177.081,604.89,10,0q�6<   $	��Top=�6Y   $��Normal"�6q   $��Italic$�6�   
���   �6�   �� 5!      -   5        
 ��Name$
 btnGeneratePdf��5!      $��Generar PDF��6%   $��	0,0,79,10q�6;    � Cursor$
 Hand��6N   $��6��6\   $��Right=�6i   $��95��6�   $��29��6�   $��Bottom=�6�   $��1��6�   $��1��6�   �� 5"      -   5        $
 	btnCancel��5"      $��Cerrar��6    $��	0,0,10,10q�61   $
 Hand��6D   $��7��6R   $��Right=�6_   $��64��6{   $��29��6�   $��Bottom=�6�   $��1��6�   $��1��6�   �� 5#      -   5        $ lblFolioNumber��5#      $�� ��6$   $��27��6/   $��0,8,10,0q�6;   $	��Top=�6M   $��Right=�6e   $��81.253��6�   $��1��6�   �� 5$      -   5        $
 btnSendMail��5$      $��Enviar correo��6"   $��
0,0,179,10q�6:   $
 Hand��6N   $��6��6\   $��Right=�6i   $��95.104��6�   $��29��6�   $��Bottom=�6�   $��1��6�   $��1��6�   �� 5%      -   5        $# lblAccountStatusNumberCaption��5%      $��Número de folio:��63   $��27��6N   $��0,8,96.253,0q�6Z   $	��Top=�6p   $��Right=�6�   $
��Bold"�6�   $��1��6�   $��Right=�6�   $��112.747��6�   �� 5&      -   5        $ 	lblStatus��5&      $��Enviando correo...��6   $��Right=�6<   $��	0,0,10,44q�6X   $��123.168��6k   
���  ��$
��Bold"�6�   $��Italic$�6�   $��13.333(�6�   $��29��6�   $��Bottom=�6�   $��1��6�   $��1��6�   $y�Hidden=�6  �� 5'      -   5        $ lblPatientName��5'      $�� ��6$   $��27��6/   $��76.581,8,262.027,0q�6;   $	��Top=�6W   $��2��6o   &   System.Windows.Controls.DataGrid  5(      -   5          Name$ dgTreatments��5(      $��10,88.924,10,0q�6%      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�5*         CanUserAddRows .     CanUserDeleteRows .  $��1��5+      $��2��6   $��191.791��6'   $	��Top=�68   ��5)      �� *ObservableData+   Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Tratamiento��6      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.4* 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5.   	   `� 5/   
    �� StringFormat$ {0} - {1} ({2})��6   .1�50      ��  �� Path$! TreatmentPrice.TreatmentKey�6   �� 51      $ TreatmentPrice.Name�6   �� 52      $ TreatmentPrice.Type�6   53      55   	     56      $ Cantidad��68   $
 0.2* 6J    6   �� *
Quantity+6J     57      $ Precio��6L   $
 0.2* 6\    6   �� *Price+$ ${0:n}��6\     58      $ 	Descuento��6   $ 0.25* 6.    59   	   `� 5:   
   $
 {0}%��6   .1�5;      �� $ Discount�6   5<      5>   	     5?      $ Total��6L   $ 0.25* 6[    6   �� *Total+$ ${0:n}��6[     5@      $ Fecha��6   $ 0.22* 6*    5A   	   `� 5B   
    `� 	Converter# [  6   .1�5C      �� $ TreatmentDate�6   5D      5F   	   5H        5I      -   5        $ 
dgPayments��5I      $��10,325.882,10,0q�6#    .  .  .  $ Single=�5K       .   .  $��1��5L      $��2��6   $��191.791��6'   $	��Top=�68   ��5J      �� *ObservableData+ 5M        5N      $ Tipo de pago��64   $
 0.2* 6J    6   �� *Type+6J     5O      $ Banco��69   $ 0.18* 6H    6   �� *	Bank.Name+6H     5P      $ Cantidad��6M   $ 0.18* 6_    6   �� *Amount+$ ${0:n}��6_     5Q      $ No. de voucher o cheque��6B   $ 0.35* 6c    6   �� *VoucherCheckNumber+6c     5R      $ Fecha de pago��6   $ 0.22* 62    5S   	   `� 5T   
   # [  6   .1�5U      �� $ PaymentDate�6   5V      5X   	     5Y      $ Observaciones��6;   $ 0.25* 6R    6   �� *Observation+6R   5[      �� 5\      -   5        $ lblStatementMessage��5\      $_��Y**Los tratamientos y pagos de este folio fueron 
agregados al estado de cuenta número: ��6)   $
��Left=�6�   $��44��6�   $��	10,19,0,0q�6�   $	��Top=�6�   $	��297��6�   $��1��6�   $��Italic$�6�   $y�Hidden=�6  5]      5^      5_      5`      