   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem /    )MyDentApplication.FindTreatmentPriceModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$ Buscar tratamiento��$	��652��$��437.791�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ 	btnAccept��$��Aceptar��$��	0,0,75,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ 	btnCancel��$��Cancelar��$��	0,0,10,10q�$
 Hand��$��6��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnViewAllTreatmentPrices��$��63.333��$	��Top=�$��0,14.5,10,0q�$��25��$��Right=�$��Ver todo��$
 Hand��$��3���� -    ��Name$ cbFilter��$��26��$��84,16.02,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$	��156�� �� SelectedIndex$ 0��.@���	Clave����Nombre del tratamiento����	Área���� $��69��$	��Top=�$��10,14.02,0,0q�$��27��$
��Left=�$��Buscar por:���� -   	 ��Name$	 txtSearchTerm��$
��Left=�$��25.02��$��
245,16,0,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$	��166��
���   �$��1���� -   $ btnRefreshTreatments��$��
416,16,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��&   System.Windows.Controls.DataGrid  -	     Name$ dgTreatmentPrices��$��10,50.353,10,44q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Clave��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Área��$
 0.3*  �� *Type+  $ Precio��$
 0.2*  �� *Price+ �� StringFormat$ ${0:n}��  $ Descuento (%)��$ 0.22*  �� *
Discount+