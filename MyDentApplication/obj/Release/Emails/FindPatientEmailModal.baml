   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication-   'MyDentApplication.FindPatientEmailModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$ Seleccionar paciente��$	��652��$��504.955�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ 
dgPatients��$��10,58,10,44q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .  	   CanUserAddRows	 .  
   CanUserDeleteRows
 .  $��4������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Exp. No.��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.07* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
AssignedId+  $ 	Nombre(s)��$ 0.15*  �� *	FirstName+  $ Apellido(s)��$ 0.15*  �� *
LastName+  $ Email��$ 0.15*  �� *Email+�� -    ��Name$ 	btnAccept��$��Aceptar��$��	0,0,75,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ 	btnCancel��$��Cancelar��$��	0,0,10,10q�$
 Hand��$��6��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnViewAllPatients��$��63.333��$	��Top=�$��0,14.5,10,0q�$��25��$��Right=�$��Ver todo��$
 Hand��$��3���� -    ��Name$ cbFilter��$��26��$��84,16.02,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��100.5�� �� SelectedIndex$ 0��.@���	Nombre(s)����Apellido(s)����	Email���� $��69��$	��Top=�$��10,14.02,0,0q�$��27��$
��Left=�$��Buscar por:���� -    ��Name$ txtSearchTerm��$
��Left=�$��25.02��$��
192,16,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��166��
���   �$��1���� -	   $ btnRefreshPatients��$��
363,16,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��