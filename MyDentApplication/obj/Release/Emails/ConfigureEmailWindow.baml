   M S B A M L   `   `   `  ����   MyDentApplication,    &MyDentApplication.ConfigureEmailWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$! Configurar cuenta de correo��$��593.5��$	��282�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ 	btnAccept��$��Aceptar��$��	0,0,75,10q� � Cursor$
 Hand��$��8��$��29��$��Bottom=�$��Right=�$��60���� -   $ 	btnCancel��$��Cancelar��$��	0,0,10,10q�$
 Hand��$��9��$��29��$��Bottom=�$��Right=�$��60���� $��
10,10,0,10q�$*��$Información del servidor de correos��$
��Bold"�
���   �
���   �$
��Left=�$��230.667��.��� $��0,10,0,0q�.+��� -    ��Name$ lblExpiredMedicine_Copy3��$��16.5,13.04,0,0q�$��Normal"�$��Host:��$
��Left=�$��37.66��$��27��$	��Top=��� -   	 ��Name$	 txtHost��
 �� TextWrapping$

 Wrap=�$��59.16,13.04,0,0q�$��Center=�
���   �$��Normal"�$��27��$	��Top=�$
��Left=�$��149.507��$��0���� -   $ lblExpiredMedicine_Copy��$��7.167,45.04,0,0q�$��Normal"�$��Puerto:��$
��Left=�$��46.993��$��27��$	��Top=��� -   $ lblExpiredMedicine_Copy1��$��21.834,77.04,0,0q�$��Normal"�$
��SSL:��$
��Left=�$��32.326��$��27��$	��Top=��� -	   $	 txtPort��$

 Wrap=�$��59.16,45.04,0,0q�$��Center=�
���   �$��Normal"�$
��Left=�$��149.507��$��27��$	��Top=�$��1���� -
    ��Name$ chkSsl��$��59.16,84.04,0,0q�$��CheckBox��$
��Left=�$��13.757��$��15.25��$	��Top=�$��2���� -   $ lblExpiredMedicine_Copy6��$��7.167,121.04,0,0q�$��Normal"�$+��%Utilizar información por defecto de:��$
��Left=�$��201.5��$��27��$	��Top=��� -   $ btnGmailDefaultInfo��$��Gmail��$��120.667,153.04,0,0q�$
 Hand��$��4��$��Normal"�$
��Left=�$��60��$��29��$	��Top=��� -   $ btnHotmailDefaultInfo��$��Hotmail��$��34,153.04,0,0q�$
 Hand��$��3��$��Normal"�$
��Left=�$��60��$��29��$	��Top=��� $)��#Información de la cuenta de correo��$��245.667,10,10,44q�$
��Bold"�
���   �
���   �.��� -   $ grdEmailAccountInfo��$��0,10,0,0q�.+��� -   $	 txtUsername��$��87.667,26.04,22.667,0q�$

 Wrap=�
���   �$��Normal"�$��27��$	��Top=�$��5���� -   $ lblExpiredMedicine_Copy2��$��Correo:��$��	32,26,0,0q�$��Normal"�$
��Left=�$��51��$��25��$	��Top=��� -   $ lblExpiredMedicine_Copy4��$��Contraseña:��$��9.174,62.043,0,0q�$��Normal"�$
��Left=�$��75.493��$��25��$	��Top=�G� -    G�Name$ 
pbPassword��$��87.667,62.043,22.667,0q�$��6��
���   �$��Normal"�$��27��$	��Top=��� -   $ lblExpiredMedicine_Copy5��$��Repetir
contraseña:��$��9.174,90.04,0,0q�$��Normal"�$��43��$	��Top=�$
��Left=�$��75.493��G� -   $ pbRepeatPassword��$��87.667,98.04,22.667,0q�$��7��
���   �$��Normal"�$��27��$	��Top=�