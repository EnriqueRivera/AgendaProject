   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      /    )MyDentApplication.ViewPaymentFoliosWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Historial de folios��5      $	��703��5      $	��496��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6Z    � 	Resources 5	      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5
      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgFolioNumbers��5      $��10,152,10,44q�6%      
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ No. de folio��69      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *FolioNumber+6O     5      $ Fecha de generación��6   $ 0.17* 66    5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ 	FolioDate�6   5   	   5        5      $ Paciente��6   $ 0.15* 6+    5      `� 5       �� StringFormat$ (Exp. No. {0}) {1} {2}��6   .1�5      �� $ Patient.AssignedId�6&   �� 5   	   $ Patient.FirstName�6   �� 5   	   $ Patient.LastName�6   5    	   5"        5#      $" Usuario que generó el folio��6   $ 0.22* 6>    5$      `� 5%      $ {0} {1}��6   .1�5&   	   �� $ User.FirstName�6   �� 5'   	   $ User.LastName�6   5(   	   5*        5+      $ Total de tratamientos��6E   $ 0.18* 6d    6   �� *TreatmentPayments.Count+6d     5,      $ Total de pagos��6<   $ 0.13* 6T    6   �� *Payments.Count+6T   5.      �� 5/      $ ��Búsqueda por No. de folio��6   $
��Left=�60   $	��137��6K   $��	10,10,0,0q�6X   $	��Top=�6k   $	��254��6�   
���   �$
��Bold"�6�   .��50      � $��0,0,-2,-3.96q�6
   .+�51      �� -   5         ��Name$ 
lblPatient��51      $��Número de folio:��6    $
��Left=�6;   $��27��6V   $��16.5,28.01,0,0q�6b   $	��Top=�6z   $��99.333��6�   $��Normal"�6�   �� 52      -   5         ��Name$ 	btnSearch��52      $��Buscar��6    $��88.166,67.04,0,0q�61    � Cursor$
 Hand��6K   $��7��6Y   $
��Left=�6f   $
��68.5��6�   $��29��6�   $	��Top=�6�   $��Normal"�6�   �� 53      -   5         ��Name$ txtFolioNumber��53      $
��Left=�6&   $��27��6A   $��120.833,28.01,0,0q�6M    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $��102.833��6�   $��1��6�   
���   �$��Normal"�6�   54      55      �� 56      $��Filtrar folios por fecha��6   $��Right=�6/   $	��137��6K   $��	0,10,10,0q�6X   $	��Top=�6k   $��353.5��6�   
���   �$
��Bold"�6�   .��57      � $��0,0,-2,-3.96q�6
   .+�58      �� -   5        $ 	btnFilter��58      $��Filtrar��6    $��145,76.54,0,0q�62   $
 Hand��6I   $��7��6W   $
��Left=�6d   $
��68.5��6   $��29��6�   $	��Top=�6�   $��Normal"�6�   (  "System.Windows.Controls.DatePicker  59      -   5          Name$ dpStartDate��59      $
��Left=�6&   $��114.5,17.54,0,0q�6A   $	��Top=�6Z   $��212.64��6r   $��Normal"�6�     5:      -	   5        $ 	dpEndDate��5:      $
��Left=�6$   $��114.5,47.54,0,0q�6?   $	��Top=�6X   $��212.64��6p   $��Normal"�6   �� 5;      $��Fecha de inicio:��6   $
��Left=�6'   $��24��6B   $��16.5,17.54,0,0q�6N   $	��Top=�6f   $��93��6~   $��Normal"�6�   �� 5<      $��Fecha de fin:��6   $
��Left=�6$   $��24��6?   $��31.833,46.54,0,0q�6K   $	��Top=�6e   $��77.667��6}   $��Normal"�6�   5=      5>      �� 5?      -
   5        $ btnViewFolioDetails��5?      $��Ver detalles��6(   $��	0,0,10,10q�6?   $
 Hand��6R   $��7��6`   $��Right=�6m   $
��85.5��6�   $��29��6�   $��Bottom=�6�   $��Normal"�6�   5@      5A      