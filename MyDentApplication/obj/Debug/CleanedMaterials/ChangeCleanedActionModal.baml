   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication0   *MyDentApplication.ChangeCleanedActionModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$ Marcar registro como ��5      $	��376��5      $��378.5��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � 
ResizeMode$ CanMinimize=�6U    � WindowStartupLocation$ CenterScreen=�6n   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ txtDate��5      $��27��6   $��64.333,10,10,0q�6)    �� TextWrapping$
 Wrap=�6A   $	��Top=�6U   $��4��6m   $��1q�6z   
���   �{�.  $��Right=�6�    �� TextAlignment$ Center=�6�   �� 5      -   5        	 ��Name$	 lblDate��5   
   $��Fecha:��6   $
��Left=�6,   $��27��6G   $��	10,10,0,0q�6S   $	��Top=�6f   $��49.333��6~   �� 5      -   5        $"	 lblCleanedActionObservations��5   
   $��Observaciones:��60   $
��Left=�6I   $��25.672��6d   $��10,133.276,0,0q�6t   $	��Top=�6�   $��98.408��6�   �� 5      $��1q�6   
���   �$��10,163.948,10,0q�63   $��118.835��6L   $	��Top=�6]   ._�5      �� -   5        $" txtCleanedActionObservations��5      $��0,0,0,0q�63   $��Italic$�6D   "
 �� VerticalScrollBarVisibility$

 Auto=�6W   $
 Wrap=�6z    �� AcceptsReturn . 6�   5      �� 5      -   5        $	 lblShift��5   
   $��Turno:��6   $
��Left=�6-   $��27��6H   $��10,55.333,0,0q�6T   $	��Top=�6k   $��49.333��6�   �� 5      -   5         ��Name$ cmbShift��5      $��64.333,55.333,10,0q�6   $	��Top=�6;   $��27��6S    �� SelectedIndex$ 0��6_   .@�5      ��Matutino��6   ��5      
Vespertino��6   5      �� 5      -	   5         ��Name$ btnAddCleanedAction��5      $��
Actualizar��6(   $
��Left=�6=   $��29��6X   $��83.333,300.197,0,0q�6d   $	��Top=�6�   $��91��6�    � Cursor$
 Hand��6�   $��7��6�   6�   �� 5      -
   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��192.333,300.197,0,0q�6X   $	��Top=�6u   $��91��6�   $
 Hand��6�   $��8��6�   6�   �� 5      -   5        $ txtGroup��5      $��27��6   $��64.333,101.276,10,0q�6*   $
 Wrap=�6G   $	��Top=�6[   $��4��6s   $��1q�6�   
���   �{�.  $��Right=�6�   $ Center=�6�   �� 5      -   5        $	 lblGroup��5   
   $��Grupo:��6   $
��Left=�6-   $��27��6H   $��10,101.276,0,0q�6T   $	��Top=�6l   $��49.333��6�   5      5      