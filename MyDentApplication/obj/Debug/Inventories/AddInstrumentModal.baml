   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication*   $MyDentApplication.AddInstrumentModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$ Agregar instrumento��5      $	��390��5      $	��232��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S   .��5	      � -   5         �Name$ 
LayoutRoot��5	      $��	0,0,-6,-6q�6   .+�5
      �� -   5         ��Name$ lblProviderName��5
   
   $��Nombre del instrumento:��6#   $
��Left=�6E   $��27��6`   $��	10,10,0,0q�6l   $	��Top=�6   $��143.5��6�   �� 5      -   5         ��Name$ btnAddInstrument��5      $��Agregar��6%   $
��Left=�67   $��29��6R   $��84.67,151.697,0,0q�6^   $	��Top=�6y   $��91��6�    � Cursor$
 Hand��6�   $��6��6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��193.67,151.697,0,0q�6X   $	��Top=�6t   $��91��6�   $
 Hand��6�   $��7��6�   6�   �� 5      -   5        $ lblFolio��5   
   $��	Cantidad:��6   $
��Left=�60   $��27��6K   $��93.503,42,0,0q�6W   $	��Top=�6n   $��59.997��6�   �� 5      -   5        $ lblTotalAmount��5   
   $��No. de usos:��6"   $
��Left=�69   $��27��6T   $��75.833,106,0,0q�6`   $	��Top=�6x   $��77.667��6�   �� 5      -   5         ��Name$ 
txtMaxUses��5      $��27��6    $��158.5,105,14.5,0q�6,   	 �� TextWrapping$
	 Wrap=�6F   $	��Top=�6Z   $��5��6r   $��1q�6   
���   �6�   �� 5      -	   5        $ lblPaidMethod��5   
   $��	Usado en:��6!   $
��Left=�65   $��27��6P   $��86.67,74,0,0q�6\   $	��Top=�6r   $��66.83��6�   �� 5      -
   5        
 ��Name$
 cbTreatments��5      $��26��6#   $��158.5,74,14.5,0q�6/   $	��Top=�6H   $��Normal"�6`   $��4��6t   6�   �� 5      -   5        $ txtInstrumentName��5      $��27��6'   $��158.5,10,14.5,0q�63   $
	 Wrap=�6L   $	��Top=�6`   $��1��6x   $��1q�6�   
���   �6�   �� 5      -   5        $ txtQuantity��5      $��27��6!   $��158.5,42,14.5,0q�6-   $
	 Wrap=�6F   $	��Top=�6Z   $��1��6r   $��1q�6   
���   �6�   5      5      