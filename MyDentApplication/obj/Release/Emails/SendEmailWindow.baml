   M S B A M L   `   `   `  ���� S  OWpfRichText, Version=1.0.16.0, Culture=neutral, PublicKeyToken=75ccead1e6af2af3>.clr-namespace:WpfRichText;assembly=WpfRichTextWpfRichText  L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication '   !MyDentApplication.SendEmailWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     FWpfRichText.clr-namespace:WpfRichText;assembly=WpfRichText     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Enviar correo��$	��640��$��544.5�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot�� � RowDefinitions �� $
��364* ��� $
��151* �.+��� 
���   �$��1q�$��73.16,138.5,10,4.5q�._�    WpfRichText.RichTextEditor  -     Name$ rteBody��$��-1,-1,0.25,-1q�$��6���� -    ��Name$ btnSendEmail��$��Enviar correo��$��Right=�$��	0,0,67,10q�$��91�� � Cursor$
 Hand��$��9��$��29��$��Bottom=�	 K� ZIndex$	 6��$��1���� -   
 ��Name$
 lblExpiredMedicine��$��Para:��$��27��$��25.832,42,0,0q�$	��Top=�$
��Left=�$��39.66��$��Normal"��� -   $
 lblExpiredMedicine_Copy2��$	��De:��$��27��$��35.832,10,0,0q�$	��Top=�$��Normal"�$
��Left=�$��29.66���� -    ��Name$ txtFrom��$��27��$��73.16,10,10,0q� �� TextWrapping$
 Wrap=�$	��Top=�{�.  $��0���� -   $
 lblExpiredMedicine_Copy3��$��Asunto:��$��27��$��13.832,106.5,0,0q�$	��Top=�$
��Left=�$��51.66��$��Normal"��� -	   $ 
txtSubject��$��27��$��73.16,106.5,10,0q�$
 Wrap=�$	��Top=�
���   �$��5���� 
���   �$��1q�$
��59.5��$��73.16,42,138.667,0q�$	��Top=�._��� $��	0,0,-1,-1q�$
��Auto=�$��Disabled=�$��0q�$��1��.���� -
    ��Name$ 	spEmailTo�� �� Orientation$ 
Horizontal=��� -   $ btnSearchFiles��$��Right=�$��30��$��0,21.78,10,0q�$	��Top=�$��30��$��1��$
 Hand��$��8��.���� $;��5/MyDentApplication;component/Images/findFile_icon.png��$
��Fill=��� -   $
 lblExpiredMedicine_Copy��$��	Adjuntar:��$��27��$��
10,6.5,0,0q�$	��Top=�$
��Left=�$��58.16��$��Normal"�$��1���� 
���   �$��1q�$
��59.5��$��73.16,6.5,45,0q�$	��Top=�$��1��._��� $��	0,0,-1,-1q�$
��Auto=�$��Disabled=�$��0q�$��7��.���� -   $ spAttachedFiles��$ 
Horizontal=��� -   $ btnAddEmail��$��Right=�$��30��$��0,69.499,74.664,0q�$	��Top=�$��30��$
 Hand��$��3��.���� $;��5/MyDentApplication;component/Images/addEmail_icon.png��$
��Fill=��� -   $ btnFindEmail��$��Right=�$��30��$��0,69.499,39.664,0q�$	��Top=�$��30��$
 Hand��$��4��.���� $<��6/MyDentApplication;component/Images/findEmail_icon.png��$
��Fill=��� -    ��Name$ chkAllPatients��$��Todos los pacientes��$��16��$��0,44.001,10,0q�$	��Top=�$��Right=�$��123.667��$��2���� -   $
 lblExpiredMedicine_Copy1��$��Mensaje:��$��27��$��10,138.5,0,0q�$	��Top=�$
��Left=�$��58.16��$��Normal"��� -   $ btnClose��$��Cerrar��$��Right=�$��	0,0,10,10q�$��52��$
 Hand��$��10��$��29��$��Bottom=�$	 6��$��1���� -   $
 	lblStatus��$��Enviando correo...��$
��Left=�$��	10,0,0,10q�$��1��$��123.168��
���  ��$
��Bold"�$��Italic$�$��13.333(�$��29��$��Bottom=�$y�Hidden=��� -   $
 lblFileSizeStatus��$�� ��$
��Left=�$��29.374��$��73.16,66,0,0q�$��1��$	��Top=�$��513.84��