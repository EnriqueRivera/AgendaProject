   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  /    )MyDentApplication.ViewPaymentFoliosWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Historial de folios��$	��703��$	��496�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgFolioNumbers��$��10,152,10,44q�   
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ No. de folio��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *FolioNumber+  $ Fecha de generación��$ 0.17*  `�  `� 	Converter# [  .1���  �� Path$ 	FolioDate�  $ Paciente��$ 0.15*  `�  �� StringFormat$ (Exp. No. {0}) {1} {2}��.1��� $ Patient.AssignedId��� $ Patient.FirstName��� $ Patient.LastName�  $" Usuario que generó el folio��$ 0.22*  `� $ {0} {1}��.1��� $ User.FirstName��� $ User.LastName�  $ Total de tratamientos��$ 0.18*  �� *TreatmentPayments.Count+  $ Total de pagos��$ 0.13*  �� *Payments.Count+�� $ ��Búsqueda por No. de folio��$
��Left=�$	��137��$��	10,10,0,0q�$	��Top=�$	��254��
���   �$
��Bold"�.��� $��0,0,-2,-3.96q�.+��� -    ��Name$ 
lblPatient��$��Número de folio:��$
��Left=�$��27��$��16.5,28.01,0,0q�$	��Top=�$��99.333��$��Normal"��� -    ��Name$ 	btnSearch��$��Buscar��$��88.166,67.04,0,0q� � Cursor$
 Hand��$��7��$
��Left=�$
��68.5��$��29��$	��Top=�$��Normal"��� -    ��Name$ txtFolioNumber��$
��Left=�$��27��$��120.833,28.01,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��102.833��$��1��
���   �$��Normal"��� $��Filtrar folios por fecha��$��Right=�$	��137��$��	0,10,10,0q�$	��Top=�$��353.5��
���   �$
��Bold"�.��� $��0,0,-2,-3.96q�.+��� -   $ 	btnFilter��$��Filtrar��$��145,76.54,0,0q�$
 Hand��$��7��$
��Left=�$
��68.5��$��29��$	��Top=�$��Normal"�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$
��Left=�$��114.5,17.54,0,0q�$	��Top=�$��212.64��$��Normal"�  -	   $ 	dpEndDate��$
��Left=�$��114.5,47.54,0,0q�$	��Top=�$��212.64��$��Normal"��� $��Fecha de inicio:��$
��Left=�$��24��$��16.5,17.54,0,0q�$	��Top=�$��93��$��Normal"��� $��Fecha de fin:��$
��Left=�$��24��$��31.833,46.54,0,0q�$	��Top=�$��77.667��$��Normal"��� -
   $ btnViewFolioDetails��$��Ver detalles��$��	0,0,10,10q�$
 Hand��$��7��$��Right=�$
��85.5��$��29��$��Bottom=�$��Normal"�