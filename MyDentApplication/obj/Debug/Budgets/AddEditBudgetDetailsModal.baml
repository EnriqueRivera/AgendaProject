   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication1   +MyDentApplication.AddEditBudgetDetailsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$% Agregar concepto de presupuesto��5      $	��369��5      $	��296��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � 
ResizeMode$ CanMinimize=�6S    � WindowStartupLocation$ CenterScreen=�6l   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ btnAddUpdateBudgetDetail��5      $��Agregar��6-   $��90.17,0,0,17q�6?    � Cursor$
 Hand��6U   $��6��6c   $
��Left=�6�   $��91��6�   $��29��6�   $��Bottom=�6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��186.17,0,0,17q�61   $
 Hand��6H   $��7��6V   $
��Left=�6{   $��91��6�   $��29��6�   $��Bottom=�6�   �� 5      -   5         ��Name$ lblTreatmentDuration��5   
   $��
Descuento:��6(   $
��Left=�6=   $��27��6X   $��41.5,169,0,0q�6d   $	��Top=�6z   $��71.333��6�   �� 5      -   5        	 ��Name$	 
cbDiscount��5      $
��Left=�6!   $��28��6<   $��117.833,169,0,0q�6H   $	��Top=�6a   $��96.837��6y   
 �� SelectedIndex$
 0��6�   $��Right=�6�   $��Center=�6�   $��5��6�   .@�5      ��0��6   ��5      5��6   ��5      10��6   ��5      15��6   ��5      20��6   ��5      25��6   ��5      30��6   5      �� 5      -   5        $ lblMinutesCaption��5   
   $��%��6%   $
��Left=�61   $��27��6L   $��214.67,169,0,0q�6X   $	��Top=�6p   $	��116��6�   �� 5      -   5        $ lblRecurrentDays��5   
   $��Costo u. normal: $��6$   $
��Left=�6A   $��27��6\   $��
10,137,0,0q�6h   $	��Top=�6|   $��107.833��6�   �� 5      -	   5         ��Name$ txtCost��5      $
��Left=�6   $��27��68   $��117.833,137,0,0q�6D    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $��96.837��6�   $��4��6�   $��1q�6�   
���   �$��Center=�6�   �� 5      -
   5        $ lblTreatmentName��5   
   $��	Concepto:��6$   $
��Left=�68   $��27��6S   $��	10,10,0,0q�6_   $	��Top=�6r   $	��344��6�   �� 5      -   5        $ lblRecurrentDays_Copy��5   
   $��	Cantidad:��6)   $
��Left=�6=   $��27��6X   $��51.333,73,0,0q�6d   $	��Top=�6{   $
��61.5��6�   �� 5      -   5        $ txtQuantity��5      $
��Left=�6!   $��27��6<   $��117.833,73,0,0q�6H   $
 Wrap=�6`   $	��Top=�6t   $��96.837��6�   $��2��6�   $��1q�6�   
���   �$��Center=�6�   �� 5      -   5        $ lblRecurrentDays_Copy1��5   
   $��No. de citas:��6*   $
��Left=�6B   $��27��6]   $��36.5,105,0,0q�6i   $	��Top=�6   $��76.333��6�   �� 5      -   5        $ txtNumberOfEvents��5      $
��Left=�6'   $��27��6B   $��117.833,105,0,0q�6N   $
 Wrap=�6g   $	��Top=�6{   $��96.837��6�   $��3��6�   $��1q�6�   
���   �$��Center=�6�   �� 5      -   5        $	 
cbConcepts��5      $��28��6!   $��
10,37,10,0q�6-   $	��Top=�6A   $��Center=�6Y   $��Center=�6}   $��5��6�   6�   5       5!      