   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  (    "MyDentApplication.UserLoginsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$. (Inicios y cierres de sesión del usuario��$	��640��$	��480�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � 	Resources 8   2MyDentApplication.LoginActionAndTimeValueConverter $   LoginActionAndTimeValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  $��1��   SelectionMode$ Single=�$��10,113.593,10,10q�   
IsReadOnly . 	   CanUserSortColumns	 . 
   CanUserAddRows
 .     CanUserDeleteRows .     AutoGenerateColumns .  ���� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Acción realizada��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 1  +System.Windows.Controls.DataGridBoundColumn   Binding `�  `� 	Converter# [  .1���  �� Path$ IsLogin�  $
 Hora��$
 0.1*  `� # [  .1��� $ 	LoginDate�(  "System.Windows.Controls.DatePicker  -     Name$ dpUserLogins��$��27��$��
0,0,10,375q�$��Bottom=�$��Right=�$	��265��   SelectedDateFormat$
 Long=��� $��Seleccione un día:��$��Right=�$��27��$��0,0,166,402q�$��Bottom=�$	��109���� -    ��Name$ lblUserFirstNameCaption��$��
Nombre(s):��$��31.134,44.926,0,0q�$��Normal"�$
��Left=�$��71��$��25��$	��Top=��� -    ��Name$ lblUserFirstName��$�� ��$
��Left=�$��20��$��107.134,49.926,0,0q�$	��Top=�$��242.866��$��Normal"�$��Italic$��� -   $ lblUserLastNameCaption��$��Apellido(s):��$��32.612,69.926,0,0q�$��Normal"�$
��Left=�$��69.522��$��25��$	��Top=��� -   $ lblUserLastName��$�� ��$
��Left=�$��20��$��107.134,74.926,0,0q�$	��Top=�$��242.866��$��Normal"�$��Italic$��� -   $ lblUserIdCaption��$��No. de usuario:��$��10,19.926,0,0q�$��Normal"�$
��Left=�$��92.134��$��25��$	��Top=��� -	   $ 	lblUserId��$
��Left=�$��20��$��107.134,24.926,0,0q�$	��Top=�$��242.866��$��Normal"�$��Italic$�