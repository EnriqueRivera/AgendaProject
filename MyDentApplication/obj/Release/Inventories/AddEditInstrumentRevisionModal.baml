   M S B A M L   `   `   `  ����   MyDentApplication6    0MyDentApplication.AddEditInstrumentRevisionModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ Agregar/Editar revisión��$��401.5��$	��324�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblReminderMessage��$��Comentario:��$
��Left=�$��25.672��$��	10,10,0,0q�$	��Top=�$��99.408���� $��1q�
���   �$��10,40.672,10,0q�$��118.835��$	��Top=�._��� -    ��Name$ 
txtComment��$��0,0,0,0q�$��Italic$�" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=�	 �� AcceptsReturn	 . �� -   
 ��Name$
 btnAddEditRevision��$��Agregar/Editar��$��
0,0,106,10q� � Cursor$
 Hand��$��6��$��29��$��Bottom=�$��Right=�$��98���� -   $
 	btnCancel��$��Cancelar��$��	0,0,10,10q�$
 Hand��$��7��$��29��$��Bottom=�$��Right=�$��91���� -   $ lblProviderName��$��Cantidad actual:��$
��Left=�$��27��$��
17,181,0,0q�$	��Top=�$��96.908���� -   $ txtQuantity��$��27��$��118.908,181,106,0q�$
 Wrap=�$	��Top=�$��1��$��1q�
���   ��� -	   $ lblUses��$��Usos restantes:��$
��Left=�$��27��$��
22,213,0,0q�$	��Top=�$��91.908���� -
   $ txtUsesLeft��$��27��$��118.908,213,106,0q�$
 Wrap=�$	��Top=�$��1��$��1q�
���   ��� -   $ 
lblMaxUses��$�� ��$
��Left=�$��27��$��292.5,213,0,0q�$	��Top=�$��91��