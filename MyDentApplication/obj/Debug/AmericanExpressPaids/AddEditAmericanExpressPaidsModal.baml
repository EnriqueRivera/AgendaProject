   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication8   2MyDentApplication.AddEditAmericanExpressPaidsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$# Agregar pago American Express��5      $	��345��5      $	��238��6    � 
ResizeMode$ CanMinimize=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��64    � WindowStartupLocation$ CenterScreen=�6l   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ btnAddUpdatePaid��5      $��Agregar��6%   $
��Left=�67   $��29��6R   $��63.67,156.697,0,0q�6^   $	��Top=�6y   $��91��6�    � Cursor$
 Hand��6�   $��8��6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��172.67,156.697,0,0q�6X   $	��Top=�6t   $��91��6�   $
 Hand��6�   $��9��6�   6�   �� 5      -   5         ��Name$ lblFolio��5   
   $��Establecimiento:��6   $
��Left=�67   $��27��6R   $��14.667,44,0,0q�6^   $	��Top=�6u   $��96.333��6�   �� 5      -   5        $ lblTotalAmount��5   
   $��Total:��6"   $
��Left=�63   $��27��6N   $��70.333,108,0,0q�6Z   $	��Top=�6r   $��40.667��6�   �� 5      -   5        $ lbPurchaseDate��5   
   $��Fecha del pago:��6"   $��27��6<   $��17.667,12,0,0q�6H   $	��Top=�6_   $
��Left=�6w   $��93.333��6�   (  "System.Windows.Controls.DatePicker  5      -   5        	  Name$	 dtpPaidDate��5      $��27��6$   $��
116,13,0,0q�60   $	��Top=�6D   $��4��6\   $
��Left=�6i   $��194.5��6�   �� 5      -	   5        
 ��Name$
 txtEstablishment��5      $
��Left=�6&   $��27��6A   $��
116,45,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $��194.5��6�   $��5��6�   $��1q�6�   
���   �6�   �� 5      -
   5        $
 txtTotalAmount��5      $
��Left=�6$   $��27��6?   $��116,109,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $��194.5��6�   $��7��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ lblFolio_Copy��5   
   $��	Concepto:��6!   $
��Left=�65   $��27��6P   $��	46,76,0,0q�6\   $	��Top=�6o   $��65��6�   �� 5      -   5        $
 
txtConcept��5      $
��Left=�6    $��27��6;   $��
116,77,0,0q�6G   $
 Wrap=�6[   $	��Top=�6o   $��194.5��6�   $��5��6�   $��1q�6�   
���   �6�   5      5      