   M S B A M L   `   `   `  ����   MyDentApplication)    #MyDentApplication.ManageUsersWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$ Administrar usuarios��$	��652��$	��496�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgUsers��$��10,10,12.917,60q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .  	   SelectionMode$	 Single=�
   CanUserAddRows
 .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ No. de usuario��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.07* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *AssignedUserId+  $ 	Nombre(s)��$ 0.15*  �� *	FirstName+  $ Apellido(s)��$ 0.15*  �� *
LastName+�� -    ��Name$ btnDeleteUser��$��Eliminar��$��0,0,13.25,14.469q� � Cursor$
 Hand��$��4��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnEditUser��$��Editar��$��0,0,78.25,14.469q�$
 Hand��$��3��$��Right=�$��60��$��29��$��Bottom=��� -   $ 
btnAddUser��$��Agregar��$��Right=�$��0,0,143.25,14.469q�$��60��$
 Hand��$��2��$��29��$��Bottom=��� -   $ btnUserLogins��$"��Inicios y cierres de sesión��$��10,0,0,14.469q�$
 Hand��$��2��$��29��$��Bottom=�$
��Left=�$��156.333��