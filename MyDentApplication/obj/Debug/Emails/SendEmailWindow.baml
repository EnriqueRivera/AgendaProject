   M S B A M L   `   `   `  ����S  OWpfRichText, Version=1.0.16.0, Culture=neutral, PublicKeyToken=75ccead1e6af2af3>.clr-namespace:WpfRichText;assembly=WpfRichTextWpfRichText  5      L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication 5      '   !MyDentApplication.SendEmailWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      FWpfRichText.clr-namespace:WpfRichText;assembly=WpfRichText     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Enviar correo��5      $	��640��5	      $��544.5��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6\   .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5      �� 5      $
��364* �6   �� 5      $
��151* �6   .+�5      �� 
���   �$��1q�6   $��73.16,138.5,10,4.5q�63   ._�5          WpfRichText.RichTextEditor  -   5          Name$ rteBody��5       $��-1,-1,0.25,-1q�61   $��6��6H   5      �� 5      -   5         ��Name$ btnSendEmail��5      $��Enviar correo��6!   $��Right=�69   $��	0,0,67,10q�6U   $��91��6h    � Cursor$
 Hand��6s   $��9��6�   $��29��6�   $��Bottom=�6�   	 K� ZIndex$	 6��6�   $��1��6�   �� 5      -   5        
 ��Name$
 lblExpiredMedicine��5   
   $��Para:��6&   $��27��66   $��25.832,42,0,0q�6B   $	��Top=�6Y   $
��Left=�6q   $��39.66��6�   $��Normal"�6�   �� 5      -   5        $
 lblExpiredMedicine_Copy2��5   
   $	��De:��6,   $��27��6:   $��35.832,10,0,0q�6F   $	��Top=�6]   $��Normal"�6u   $
��Left=�6�   $��29.66��6�   �� 5      -   5         ��Name$ txtFrom��5      $��27��6   $��73.16,10,10,0q�6)    �� TextWrapping$
 Wrap=�6@   $	��Top=�6T   {�.  $��0��6~   �� 5      -   5        $
 lblExpiredMedicine_Copy3��5   
   $��Asunto:��6,   $��27��6>   $��13.832,106.5,0,0q�6J   $	��Top=�6d   $
��Left=�6|   $��51.66��6�   $��Normal"�6�   �� 5      -	   5        $ 
txtSubject��5      $��27��6    $��73.16,106.5,10,0q�6,   $
 Wrap=�6F   $	��Top=�6Z   
���   �$��5��6�   �� 5      
���   �$��1q�6   $
��59.5��63   $��73.16,42,138.667,0q�6A   $	��Top=�6]   ._�5      �� $��	0,0,-1,-1q�6   $
��Auto=�6%   $��Disabled=�6J   $��0q�6q   $��1��6�   .��5      �� -
   5         ��Name$ 	spEmailTo��5       �� Orientation$ 
Horizontal=�6$   5      5      �� 5      -   5        $ btnSearchFiles��5      $��Right=�6#   $��30��6?   $��0,21.78,10,0q�6K   $	��Top=�6a   $��30��6y   $��1��6�   $
 Hand��6�   $��8��6�   .��5      �� $;��5/MyDentApplication;component/Images/findFile_icon.png��6   $
��Fill=�6J   5       �� 5!      -   5        $
 lblExpiredMedicine_Copy��5!   
   $��	Adjuntar:��6+   $��27��6?   $��
10,6.5,0,0q�6K   $	��Top=�6_   $
��Left=�6w   $��58.16��6�   $��Normal"�6�   $��1��6�   �� 5"      
���   �$��1q�6   $
��59.5��63   $��73.16,6.5,45,0q�6A   $	��Top=�6Y   $��1��6q   ._�5#      �� $��	0,0,-1,-1q�6   $
��Auto=�6%   $��Disabled=�6J   $��0q�6q   $��7��6�   .��5$      �� -   5        $ spAttachedFiles��5$      $ 
Horizontal=�6*   5%      5&      �� 5'      -   5        $ btnAddEmail��5'      $��Right=�6    $��30��6<   $��0,69.499,74.664,0q�6H   $	��Top=�6c   $��30��6{   $
 Hand��6�   $��3��6�   .��5(      �� $;��5/MyDentApplication;component/Images/addEmail_icon.png��6   $
��Fill=�6J   5)      �� 5*      -   5        $ btnFindEmail��5*      $��Right=�6!   $��30��6=   $��0,69.499,39.664,0q�6I   $	��Top=�6d   $��30��6|   $
 Hand��6�   $��4��6�   .��5+      �� $<��6/MyDentApplication;component/Images/findEmail_icon.png��6   $
��Fill=�6K   5,      �� 5-      -   5         ��Name$ chkAllPatients��5-      $��Todos los pacientes��6%   $��16��6C   $��0,44.001,10,0q�6O   $	��Top=�6f   $��Right=�6�   $��123.667��6�   $��2��6  �� 5.      -   5        $
 lblExpiredMedicine_Copy1��5.   
   $��Mensaje:��6,   $��27��6?   $��10,138.5,0,0q�6K   $	��Top=�6a   $
��Left=�6y   $��58.16��6�   $��Normal"�6�   �� 5/      -   5        $ btnClose��5/      $��Cerrar��6   $��Right=�6.   $��	0,0,10,10q�6J   $��52��6]   $
 Hand��6h   $��10��6v   $��29��6�   $��Bottom=�6�   $	 6��6�   $��1��6�   6�   �� 50      -   5        $
 	lblStatus��50   
   $��Enviando correo...��6   $
��Left=�6:   $��	10,0,0,10q�6U   $��1��6h   $��123.168��6u   
���  ��$
��Bold"�6�   $��Italic$�6�   $��13.333(�6�   $��29��6�   $��Bottom=�6�   $y�Hidden=�6�   �� 51      -   5        $
 lblFileSizeStatus��51   
   $�� ��6%   $
��Left=�60   $��29.374��6K   $��73.16,66,0,0q�6[   $��1��6q   $	��Top=�6~   $��513.84��6�   52      53      