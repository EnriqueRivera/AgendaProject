   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ,    &MyDentApplication.ManageContactsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$ Administrar contactos��$��696.716��$	��480�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ 
dgContacts��$��10,52,10,46q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .  	   SelectionMode$	 Single=�
   CanUserAddRows
 .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Nombre(s)��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	FirstName+  $ Apellido(s)��$ 0.11*  �� *
LastName+  $ 
Dirección��$
 0.2*  �� *	Address+  $ Teléfono celular��$ 0.11*  �� *	CellPhone+  $ Teléfono de casa��$ 0.11*  �� *	HomePhone+�� -    ��Name$ btnDeleteContact��$��Eliminar��$��	0,0,10,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnEditContact��$��Editar��$��	0,0,75,10q�$
 Hand��$��4��$��Right=�$��60��$��29��$��Bottom=��� -   $ btnAddContact��$��Agregar��$��Right=�$��
0,0,140,10q�$��60��$
 Hand��$��3��$��29��$��Bottom=��� -   $ btnViewAllContacts��$��Ver todo��$��25��$��0,14.5,10,0q�$	��Top=�$��Right=�$��63.333��$
 Hand���� -    ��Name$ cbFilter��$��26��$��84,16.02,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��100.5�� �� SelectedIndex$ 0��.@���	Nombre(s)����Apellido(s)����
Dirección���� $��69��$	��Top=�$��10,14.02,0,0q�$��27��$
��Left=�$��Buscar por:���� -	    ��Name$ txtSearchTerm��$
��Left=�$��25.02��$��
192,16,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��166��
���   ��� -
   $ btnRefreshContacts��$��
363,16,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��