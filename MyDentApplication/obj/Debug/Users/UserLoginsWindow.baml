   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      (    "MyDentApplication.UserLoginsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$. (Inicios y cierres de sesión del usuario��5      $	��640��5      $	��480��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S    � 
ResizeMode$ CanMinimize=�6x    � 	Resources 5
      8   2MyDentApplication.LoginActionAndTimeValueConverter $   LoginActionAndTimeValueConverter��%   &	          5      6+   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  $��1��6      SelectionMode$ Single=�6   $��10,113.593,10,10q�61      
IsReadOnly . 	   CanUserSortColumns	 . 
   CanUserAddRows
 .     CanUserDeleteRows .     AutoGenerateColumns .  ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Acción realizada��6      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ IsLogin�6   5   	   5        5      $
 Hora��6   $
 0.1* 6'    5      `� 5      # [  6   .1�5   	   �� $ 	LoginDate�6   5   	   5       5"      (  "System.Windows.Controls.DatePicker  5#      -   5          Name$ dpUserLogins��5#      $��27��6%   $��
0,0,10,375q�61   $��Bottom=�6E   $��Right=�6�   $	��265��6�      SelectedDateFormat$
 Long=�6�   �� 5$      $��Seleccione un día:��6
   $��Right=�6'   $��27��6C   $��0,0,166,402q�6O   $��Bottom=�6d   $	��109��6   �� 5%      -   5         ��Name$ lblUserFirstNameCaption��5%   
   $��
Nombre(s):��6+   $��31.134,44.926,0,0q�6@   $��Normal"�6[   $
��Left=�6o   $��71��6�   $��25��6�   $	��Top=�6�   �� 5&      -   5         ��Name$ lblUserFirstName��5&      $�� ��6(   $
��Left=�60   $��20��6K   $��107.134,49.926,0,0q�6W   $	��Top=�6s   $��242.866��6�   $��Normal"�6�   $��Italic$�6�   �� 5'      -   5        $ lblUserLastNameCaption��5'   
   $��Apellido(s):��6*   $��32.612,69.926,0,0q�6A   $��Normal"�6\   $
��Left=�6p   $��69.522��6�   $��25��6�   $	��Top=�6�   �� 5(      -   5        $ lblUserLastName��5(      $�� ��6'   $
��Left=�6/   $��20��6J   $��107.134,74.926,0,0q�6V   $	��Top=�6r   $��242.866��6�   $��Normal"�6�   $��Italic$�6�   �� 5)      -   5        $ lblUserIdCaption��5)   
   $��No. de usuario:��6$   $��10,19.926,0,0q�6>   $��Normal"�6U   $
��Left=�6i   $��92.134��6�   $��25��6�   $	��Top=�6�   �� 5*      -	   5        $ 	lblUserId��5*      $
��Left=�6!   $��20��6<   $��107.134,24.926,0,0q�6H   $	��Top=�6d   $��242.866��6|   $��Normal"�6�   $��Italic$�6�   5+      5,      