   M S B A M L   `   `   `  ����   MyDentApplication,    &MyDentApplication.AddEditContactsModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ Agregar contacto��$	��380��$	��347�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��$��0,0,2,2q�.+��� -    ��Name$ lblMedicineNotes��$��Dirección:��$
��Left=�$��25.672��$��10,142.657,0,0q�$	��Top=�$��95.408���� $��1q�
���   �$��10,168.329,10,52.667q�._��� -    ��Name$ txtContactAddress��$��0,0,0,0q�$��Italic$�" �� VerticalScrollBarVisibility$
 Auto=�	 �� TextWrapping$
	 Wrap=�$��5���� -   $ lblContactFirstName��$��
Nombre(s):��$
��Left=�$��27��$��46.5,10,0,0q�$	��Top=�$��68���� -   $ txtContactFirstName��$��27��$��119.5,10,10,0q�$
	 Wrap=�$	��Top=�$��1��$��1q�
���   ��� -   $ lblContactLastName��$��Apellido(s):��$
��Left=�$��27��$��46.5,42,0,0q�$	��Top=�$��68���� -   $ lblContactCellPhone��$��Teléfono celular:��$
��Left=�$��27��$��16.333,74,0,0q�$	��Top=�$��98.167���� -	   $ txtContactLastName��$��27��$��119.5,42,10,0q�$
	 Wrap=�$	��Top=�$��2��$��1q�
���   �
 K� ZIndex$
 1���� -
   $ txtContactCellPhone��$��27��$��119.5,74,10,0q�$
	 Wrap=�$	��Top=�$��3��$��1q�
���   �$
 3���� -    ��Name$ btnAddUpdateContact��$��Agregar��$��Right=�$��
0,0,106,10q�$��91�� � Cursor$
 Hand��$��6��$��29��$��Bottom=�$
 6���� -   $ 	btnCancel��$��Cancelar��$��Right=�$��	0,0,10,10q�$��91��$
 Hand��$��7��$��29��$��Bottom=�$
 7���� -   $ lblContactHomePhone��$��Teléfono de casa:��$
��Left=�$��27��$��
11,106,0,0q�$	��Top=�$��103.5���� -   $ txtContactHomePhone��$��27��$��119.5,106,10,0q�$
	 Wrap=�$	��Top=�$��4��$��1q�
���   �$
 3��