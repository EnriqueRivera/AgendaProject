   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication0   *MyDentApplication.AddEditGeneralPaidsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$ Agregar pago general��5      $	��390��5      $	��294��6    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ CanMinimize=�6@   .��5	      � -   5         �Name$ 
LayoutRoot��5	      $��	0,0,-4,-4q�6   .+�5
      �� -   5         ��Name$ lblProviderName��5
   
   $��Nombre del proveedor:��6#   $
��Left=�6C   $��27��6^   $��13.5,31,0,0q�6j   $	��Top=�6   $��134.5��6�   �� 5      -   5         ��Name$ btnAddUpdateGeneralPaid��5      $��Agregar��6,   $
��Left=�6>   $��29��6Y   $��86.67,205.697,0,0q�6e   $	��Top=�6�   $��91��6�    � Cursor$
 Hand��6�   $��6��6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��195.67,205.697,0,0q�6X   $	��Top=�6t   $��91��6�   $
 Hand��6�   $��7��6�   �� 5      -   5        $ lblFolio��5   
   $��Número de ticket:��6   $
��Left=�68   $��27��6S   $��41.667,95,0,0q�6_   $	��Top=�6v   $��106.333��6�   �� 5      -   5        $ lblTotalAmount��5   
   $��Total:��6"   $
��Left=�63   $��27��6N   $��107.333,159,0,0q�6Z   $	��Top=�6s   $��40.667��6�   �� 5      -   5        $ lbPurchaseDate��5   
   $��Fecha de compra:��6"   $
��Left=�6=   $��27��6X   $��41.667,63,0,0q�6d   $	��Top=�6{   $��106.333��6�   (  "System.Windows.Controls.DatePicker  5      -	   5          Name$ dtpPurchaseDate��5      $
��Left=�6(   $��27��6C   $��
153,64,0,0q�6O   $	��Top=�6c   $��194.5��6{   $��2��6�   �� 5      -
   5        	 ��Name$	 txtTicketNumber��5      $
��Left=�6%   $��27��6@   $��
153,96,0,0q�6L   
 �� TextWrapping$

 Wrap=�6`   $	��Top=�6t   $��194.5��6�   $��3��6�   $��1q�6�   
���   �6�   �� 5      -   5        $	 txtTotalAmount��5      $
��Left=�6$   $��27��6?   $��153,160,0,0q�6K   $

 Wrap=�6`   $	��Top=�6t   $��194.5��6�   $��5��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ lblPaidMethod��5   
   $��Método de pago:��6!   $
��Left=�6;   $��27��6V   $��41.667,127,0,0q�6b   $	��Top=�6z   $��106.333��6�   �� 5      -   5         ��Name$ cbPaidMethod��5      $��26��6#   $��153,127,0,0q�6/   $	��Top=�6D   $��Normal"�6\   $��4��6p   $
��Left=�6}   $��194.5��6�   .@�5      ��Efectivo��6   ��5      
Cheque��6   ��5      Transferencia bancaria��6+   ��5      Tarjeta crédito��6$   ��5      Tarjeta débito��6#   ��5      Especie��6   5      �� 5      -   5        $	 txtProviderName��5      $
��Left=�6%   $��27��6@   $��
153,31,0,0q�6L   $

 Wrap=�6`   $	��Top=�6t   $��194.5��6�   $��1��6�   $��1q�6�   
���   �6�   5      5      