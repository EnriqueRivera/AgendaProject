   M S B A M L   `   `   `  ����  MyDentApplication-    'MyDentApplication.AddEditMedicinesModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          �Name$  Window��5       � Title$ Agregar medicamento��5      $	��380��5      $	��410��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6   $	��380��6Z   $	��410��6i    � WindowStartupLocation$ CenterScreen=�6y   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ lblMedicineNotes��5   
   $��Observaciones:��6$   $
��Left=�6=   $��25.672��6X   $��10,142.657,0,0q�6h   $	��Top=�6�   $��95.408��6�   �� 5      $��1q�6   
���   �$��10,168.329,16,95.336q�63   ._�5      �� -   5         ��Name$ txtMedicineNotes��5      $��0,0,0,0q�6'   $��Italic$�68   " �� VerticalScrollBarVisibility$
 Auto=�6K    �� TextWrapping$
 Wrap=�6n   $��4��6�   5      �� 5      -   5        $ lblMedicineName��5   
   $��	Producto:��6#   $
��Left=�67   $��27��6R   $��66.5,10,0,0q�6^   $	��Top=�6s   $��65��6�   �� 5      -   5        $ txtMedicineName��5      $��27��6%   $��136.5,10,16,0q�61   $
 Wrap=�6H   $	��Top=�6\   $��4��6t   $��1q�6�   
���   �6�   �� 5      -   5        $ lblMedicineBrand��5   
   $��Marca:��6$   $
��Left=�65   $��27��6P   $��83.333,42,0,0q�6\   $	��Top=�6s   $��48.167��6�   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5      -   5        	  Name$	 dtudExpiredDate��5      $
��25.5��61   $��136.5,74,16,0q�6?   $	��Top=�6V   
   Format$
 	YearMonth=�6n    K� ZIndex$ 2��6�   
���   �6�   �� 5      -	   5        $ lblExpiredDate��5   
   $��Fecha de caducidad:��6"   $
��Left=�6@   $��27��6[   $��	10,74,0,0q�6g   $	��Top=�6z   $��121.5��6�   �� 5      -
   5        $ lblMedicineBatch��5   
   $��Lote:��6$   $
��Left=�64   $��27��6O   $��91.333,104.5,0,0q�6[   $	��Top=�6u   $��40.167��6�   �� 5      -   5        $ lblWasMedicineChanged��5   
   $��Reemplazado:��6)   $
��Left=�6@   $��42.092,0,0,63.336q�6[   $��89.408��6v   $��27��6�   $��Bottom=�6�   �� 5      -   5        $ txtMedicineBrand��5      $��27��6&   $��136.5,42,16,0q�62   $
 Wrap=�6I   $	��Top=�6]   $��4��6u   $��1q�6�   
���   �$ 1��6�   �� 5      -   5        $ txtMedicineBatch��5      $��27��6&   $��136.5,104.5,16,0q�62   $
 Wrap=�6L   $	��Top=�6`   $��4��6x   $��1q�6�   
���   �$ 3��6�   �� 5      -   5         ��Name$ chkWasMedicineChanged��5      $�� ��6,   $
��Left=�67   $��136.5,0,0,66.336q�6R   $��15.333��6l   $��18��6{   $��Bottom=�6�   $ 5��6�   �� 5      -   5         ��Name$ btnAddUpdateMedicine��5      $��Agregar��6)   $��Right=�6;   $��0,0,119.5,10q�6W   $��91��6m    � Cursor$
 Hand��6�   $��7��6�   $��29��6�   $��Bottom=�6�   $ 6��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��Right=�61   $��	0,0,16,10q�6M   $��91��6`   $
 Hand��6�   $��8��6�   $��29��6�   $��Bottom=�6�   $ 7��6�   5      5      