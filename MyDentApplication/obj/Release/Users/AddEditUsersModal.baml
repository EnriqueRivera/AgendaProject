   M S B A M L   `   `   `  ����   MyDentApplication)    #MyDentApplication.AddEditUsersModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$ Agregar usuario��$��374.167��$	��267�� � 
ResizeMode$ CanMinimize=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ 	lblUserId��$��No. de usuario:��$
��Left=�$��27��$��68.667,13,0,0q�$	��Top=�$��89.333���� -   $ lblUserFirstName��$��
Nombre(s):��$
��Left=�$��27��$��88.667,45,0,0q�$	��Top=�$��69.333���� -   $ lblUserLastName��$��Apellido(s):��$
��Left=�$��27��$��88.667,77,0,0q�$	��Top=�$��69.333���� -    ��Name$ 	txtUserId��$
��Left=�$��27��$��167.5,13,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��180��$��1��$��1q�
���   ��� -   $ txtUserFirstName��$
��Left=�$��27��$��167.5,45,0,0q�$
 Wrap=�$	��Top=�$	��180��$��2��
���   ��� -   $ txtUserLastName��$
��Left=�$��27��$��167.5,77,0,0q�$
 Wrap=�$	��Top=�$	��180��$��3��
���   ��� -	   	 ��Name$	 btnAddUpdateUser��$��Agregar��$
��Left=�$��29��$��91.67,187.667,0,0q�$	��Top=�$��91��
 � Cursor$

 Hand��$��6���� -
   $	 	btnCancel��$��Cancelar��$
��Left=�$��29��$��187.67,187.667,0,0q�$	��Top=�$��91��$

 Hand��$��7���� -   $ lblPassword��$��Nueva contraseña:��$
��Left=�$��27��$��46.667,109,0,0q�$	��Top=�$��111.333��G� -    G�Name$ 
pbPassword��$
��Left=�$��27��$��167.5,109,0,0q�$	��Top=�$	��180��$��4��
���   ��� -   $ lblRepeatPassword��$ ��Repetir nueva contraseña:��$
��Left=�$��27��$��
10,141,0,0q�$	��Top=�$	��148��G� -   $ pbRepeatPassword��$
��Left=�$��27��$��167.5,141,0,0q�$	��Top=�$	��180��$��5��
���   �