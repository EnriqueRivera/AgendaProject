   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication4   .MyDentApplication.AddEditOutgoingInvoicesModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$" Agregar recibo de honorarios��5      $	��390��5      $��338.5��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � 
ResizeMode$ CanMinimize=�6\    � WindowStartupLocation$ CenterScreen=�6u   .��5	      � -   5         �Name$ 
LayoutRoot��5	      $��	0,0,-2,-2q�6   .+�5
      �� -   5         ��Name$ 
lblPatient��5
   
   $��	Paciente:��6   $
��Left=�62   $��27��6M   $��87.667,31,0,0q�6Y   $	��Top=�6p   $��60.333��6�   �� 5      -   5        $ lblInvoiceDate��5   
   $��Fecha de facturación:��6"   $
��Left=�6B   $��27��6]   $��23.667,95,0,0q�6i   $	��Top=�6�   $��124.333��6�   �� 5      -   5         ��Name$ btnAddUpdateInvoice��5      $��Agregar��6(   $
��Left=�6:   $��29��6U   $��87.667,0,0,10q�6a   $��Bottom=�6x   $��91��6�    � Cursor$
 Hand��6�   $��8��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��196.667,0,0,10q�6X   $��Bottom=�6p   $��91��6�   $
 Hand��6�   $��9��6�   �� 5      -   5        $ lblFolio��5   
   $��Folio:��6   $
��Left=�6-   $��27��6H   $��105.667,159,0,0q�6T   $	��Top=�6m   $��42.333��6�   �� 5      -   5        $ lblTotalAmount��5   
   $��Total:��6"   $
��Left=�63   $��27��6N   $��107.333,223,0,0q�6Z   $	��Top=�6s   $��40.667��6�   �� 5      -	   5        	 ��Name$	 
cbPatients��5      $��26��6!   $��
153,31,0,0q�6-   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��194.5��6�   (  "System.Windows.Controls.DatePicker  5      -
   5        
  Name$
 dtpInvoiceDate��5      $
��Left=�6'   $��27��6B   $��
153,95,0,0q�6N   $	��Top=�6b   $��194.5��6z   $��3��6�   {�.  6�   �� 5      -   5        $ lbPurchaseDate��5   
   $��Fecha del pago:��6"   $
��Left=�6<   $��27��6W   $��51.667,127,0,0q�6c   $	��Top=�6{   $��96.333��6�     5      -   5        $
 dtpPaidDate��5      $
��Left=�6$   $��27��6?   $��153,128,0,0q�6K   $	��Top=�6`   $��194.5��6x   $��4��6�   �� 5      -   5         ��Name$ txtFolio��5      $
��Left=�6   $��27��69   $��153,160,0,0q�6E    �� TextWrapping$
 Wrap=�6Z   $	��Top=�6n   $��194.5��6�   $��5��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ txtTotalAmount��5      $
��Left=�6$   $��27��6?   $��153,224,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $��194.5��6�   $��7��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ lblPaidMethod��5   
   $��Método de pago:��6!   $
��Left=�6;   $��27��6V   $��41.667,191,0,0q�6b   $	��Top=�6z   $��106.333��6�   �� 5      -   5        $	 cbPaidMethod��5      $��26��6#   $��153,191,0,0q�6/   $	��Top=�6D   $��Normal"�6\   $��6��6p   $
��Left=�6}   $��194.5��6�   .@�5      ��Efectivo��6   ��5      
Cheque��6   ��5      Transferencia bancaria��6+   ��5      Tarjeta crédito��6$   ��5      Tarjeta débito��6#   ��5      Especie��6   5      �� 5      -   5        $ lblIsInvoiced��5   
   $��
Facturado:��6!   $
��Left=�66   $��27��6Q   $��81.042,63,0,0q�6]   $	��Top=�6t   $��66.958��6�   �� 5       -   5         ��Name$ chkIsInvoiced��5       $�� ��6$   $
��Left=�6/   $��18��6J   $��153,69.332,0,0q�6V   $	��Top=�6n   $��15.333��6�   $��2��6�   5!      5"      