   M S B A M L   `   `   `  ����  MyDentApplication6    0MyDentApplication.AddEditInstrumentRevisionModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          �Name$  Window��5       � Title$ Agregar/Editar revisión��5      $��401.5��5      $	��324��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6U   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      �� -   5         ��Name$ lblReminderMessage��5
   
   $��Comentario:��6&   $
��Left=�6<   $��25.672��6W   $��	10,10,0,0q�6g   $	��Top=�6z   $��99.408��6�   �� 5      $��1q�6   
���   �$��10,40.672,10,0q�63   $��118.835��6K   $	��Top=�6\   ._�5      �� -   5         ��Name$ 
txtComment��5      $��0,0,0,0q�6!   $��Italic$�62   " �� VerticalScrollBarVisibility$
 Auto=�6E    �� TextWrapping$
 Wrap=�6h   	 �� AcceptsReturn	 . 6|   5      �� 5      -   5        
 ��Name$
 btnAddEditRevision��5      $��Agregar/Editar��6'   $��
0,0,106,10q�6@    � Cursor$
 Hand��6T   $��6��6b   $��29��6o   $��Bottom=�6{   $��Right=�6�   $��98��6�   6�   �� 5      -   5        $
 	btnCancel��5      $��Cancelar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��7��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��91��6�   6�   �� 5      -   5        $ lblProviderName��5   
   $��Cantidad actual:��6#   $
��Left=�6>   $��27��6Y   $��
17,181,0,0q�6e   $	��Top=�6y   $��96.908��6�   �� 5      -   5        $ txtQuantity��5      $��27��6!   $��118.908,181,106,0q�6-   $
 Wrap=�6H   $	��Top=�6\   $��1��6t   $��1q�6�   
���   �6�   �� 5      -	   5        $ lblUses��5   
   $��Usos restantes:��6   $
��Left=�65   $��27��6P   $��
22,213,0,0q�6\   $	��Top=�6p   $��91.908��6�   �� 5      -
   5        $ txtUsesLeft��5      $��27��6!   $��118.908,213,106,0q�6-   $
 Wrap=�6H   $	��Top=�6\   $��1��6t   $��1q�6�   
���   �6�   �� 5      -   5        $ 
lblMaxUses��5   
   $�� ��6   $
��Left=�6)   $��27��6D   $��292.5,213,0,0q�6P   $	��Top=�6g   $��91��6   5      5      