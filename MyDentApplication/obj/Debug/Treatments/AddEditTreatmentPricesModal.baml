   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication3   -MyDentApplication.AddEditTreatmentPricesModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$! Agregar tratamiento y costo��5      $	��426��5      $	��331��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S    � 
ResizeMode$ CanMinimize=�6x   .��5	      � -   5         �Name$ 
LayoutRoot��5	      $��	0,0,-2,-2q�6   .+�5
      �� -   5         ��Name$ btnAddUpdateTreatment��5
      $��Agregar��6*   $��110.17,0,0,10q�6<    � Cursor$
 Hand��6S   $��5��6a   $��29��6n   $��Bottom=�6z   $
��Left=�6�   $��91��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��206.17,0,0,10q�61   $
 Hand��6H   $��6��6V   $��29��6c   $��Bottom=�6o   $
��Left=�6�   $��91��6�   �� 5      -   5         ��Name$ lblTreatmentName��5   
   $��Nombre del tratamiento:��6$   $
��Left=�6F   $��27��6a   $��	10,10,0,0q�6m   $	��Top=�6�   $	��344��6�   �� 5      -   5        	 ��Name$	 txtTreatmentName��5      $��27��6&   $��
10,37,10,0q�62   
 �� TextWrapping$

 Wrap=�6F   $	��Top=�6Z   $��1��6r   $��1q�6   
���   � �� TextAlignment$ Center=�6�   �� 5      -   5        $ lblTreatmentDuration��5   
   $��
Descuento:��6(   $
��Left=�6=   $��27��6X   $��
15,178,0,0q�6d   $	��Top=�6x   $��71.333��6�   $y�Hidden=�6�   �� 5      -   5         ��Name$ 
cbDiscount��5      $
��Left=�6!   $��28��6<   $��91.333,177,0,0q�6H   $	��Top=�6`   $	��100��6x    �� SelectedIndex$ 0��6�   $��Right=�6�   $��Center=�6�   $��4��6�   $y�Hidden=�6�   .@�5      ��0��6   ��5      5��6   ��5      10��6   ��5      15��6   ��5      20��6   ��5      25��6   ��5      30��6   5      �� 5      -	   5        $ lblPercentCaption��5   
   $��%��6%   $
��Left=�61   $��27��6L   $��191.333,178,0,0q�6X   $	��Top=�6q   $	��116��6�   $y�Hidden=�6�   �� 5      -
   5        $ lblRecurrentDays��5   
   $��Costo: $��6$   $
��Left=�67   $��27��6R   $��37.833,128,0,0q�6^   $	��Top=�6v   $
��53.5��6�   �� 5      -   5        $	 txtCost��5      $
��Left=�6   $��27��68   $��91.333,128,0,0q�6D   $

 Wrap=�6\   $	��Top=�6p   $��96.837��6�   $��3��6�   $��1q�6�   
���   �$��Center=�6�   �� 5      -   5        $ lblTreatmentKey��5   
   $��Clave:��6#   $
��Left=�64   $��27��6O   $��46.833,82,0,0q�6[   $	��Top=�6r   $
��44.5��6�   �� 5      -   5        $	 txtTreatmentKey��5      $
��Left=�6%   $��27��6@   $��91.333,82,0,0q�6L   $

 Wrap=�6c   $	��Top=�6w   $��96.837��6�   $��2��6�   $��1q�6�   
���   �$��Center=�6�   �� 5      $8��2*Este tratamiento será agregado al año en curso.��6
   $
��Left=�6E   $��27��6`   $��	15,0,0,50q�6l   $��Bottom=�6   $	��355��6�   $��Italic$�6�   5      5      