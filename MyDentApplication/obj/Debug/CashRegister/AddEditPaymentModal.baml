   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication+   %MyDentApplication.AddEditPaymentModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$ Agregar pago��5      $��373.5��5      $	��306��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6\    � 
ResizeMode$ CanMinimize=�6�   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ btnAddPayment��5      $��Agregar��6"   $
��Left=�64   $��87.76,0,0,10q�6O   $��91��6e    � Cursor$
 Hand��6p   $��5��6~   $��29��6�   $��Bottom=�6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��196.76,0,0,10q�6L   $��91��6c   $
 Hand��6n   $��6��6|   $��29��6�   $��Bottom=�6�   6�   �� 5      -   5         ��Name$ lblTotalAmount��5   
   $��	Cantidad:��6"   $
��Left=�66   $��28��6Q   $��51.667,9.334,0,0q�6]   $	��Top=�6w   $��78.91��6�   $��16(�6�   �� 5      -   5        	 ��Name$	 	txtAmount��5      $��27��6   $��135.577,10.334,10,0q�6+   
 �� TextWrapping$

 Wrap=�6H   $	��Top=�6\   $��1��6t   $��1q�6�   
���   �$��16(�6�   �� 5      -   5        $ lblBank��5   
   $��Banco:��6   $
��Left=�6,   $��30��6G   $��72.004,39.001,0,0q�6S   $	��Top=�6n   $��58.573��6�   $��16(�6�   �� 5      -   5         ��Name$ cbBanks��5      $��26��6   $��135.577,43.001,10,0q�6*   $	��Top=�6G   $��Normal"�6_   $��2��6s   $��16(�6�   �� 5      -	   5        $ lblVoucherCheckNumber��5   
   $��No. de voucher:��6)   $
��Left=�6C   $��30.999��6^   $��10,73.001,0,0q�6n   $	��Top=�6�   $��120.577��6�   $��16(�6�   �� 5      -
   5        $	 txtVoucherCheckNumber��5      $��27��6+   $��135.577,74.001,10,0q�67   $

 Wrap=�6T   $	��Top=�6h   $��3��6�   $��1q�6�   
���   �$��16(�6�   �� 5      -   5        $ lblReminderMessage��5   
   $��Observaciones:��6&   $
��Left=�6?   $��10,0,0,132.328q�6Z   $��347.5��6r   $��30.672��6�   $��Bottom=�6�   $��16(�6�   �� 5      $��1q�6   
���   �$��10,0,10,50.206q�63   $��82.122��6K   $��Bottom=�6[   ._�5      �� -   5        $	 txtObservations��5      $��0,0,0,0q�6&   $��Italic$�67   " �� VerticalScrollBarVisibility$
 Auto=�6J   $

 Wrap=�6m    �� AcceptsReturn . $��4��6�   $��16(�6�   5      5      5      