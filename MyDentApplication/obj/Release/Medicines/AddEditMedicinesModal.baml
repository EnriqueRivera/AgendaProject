   M S B A M L   `   `   `  ����   MyDentApplication-    'MyDentApplication.AddEditMedicinesModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ Agregar medicamento��$	��380��$	��410�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��$	��380��$	��410�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblMedicineNotes��$��Observaciones:��$
��Left=�$��25.672��$��10,142.657,0,0q�$	��Top=�$��95.408���� $��1q�
���   �$��10,168.329,16,95.336q�._��� -    ��Name$ txtMedicineNotes��$��0,0,0,0q�$��Italic$�" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=�$��4���� -   $ lblMedicineName��$��	Producto:��$
��Left=�$��27��$��66.5,10,0,0q�$	��Top=�$��65���� -   $ txtMedicineName��$��27��$��136.5,10,16,0q�$
 Wrap=�$	��Top=�$��4��$��1q�
���   ��� -   $ lblMedicineBrand��$��Marca:��$
��Left=�$��27��$��83.333,42,0,0q�$	��Top=�$��48.167��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -   	  Name$	 dtudExpiredDate��$
��25.5��$��136.5,74,16,0q�$	��Top=�
   Format$
 	YearMonth=� K� ZIndex$ 2��
���   ��� -	   $ lblExpiredDate��$��Fecha de caducidad:��$
��Left=�$��27��$��	10,74,0,0q�$	��Top=�$��121.5���� -
   $ lblMedicineBatch��$��Lote:��$
��Left=�$��27��$��91.333,104.5,0,0q�$	��Top=�$��40.167���� -   $ lblWasMedicineChanged��$��Reemplazado:��$
��Left=�$��42.092,0,0,63.336q�$��89.408��$��27��$��Bottom=��� -   $ txtMedicineBrand��$��27��$��136.5,42,16,0q�$
 Wrap=�$	��Top=�$��4��$��1q�
���   �$ 1���� -   $ txtMedicineBatch��$��27��$��136.5,104.5,16,0q�$
 Wrap=�$	��Top=�$��4��$��1q�
���   �$ 3���� -    ��Name$ chkWasMedicineChanged��$�� ��$
��Left=�$��136.5,0,0,66.336q�$��15.333��$��18��$��Bottom=�$ 5���� -    ��Name$ btnAddUpdateMedicine��$��Agregar��$��Right=�$��0,0,119.5,10q�$��91�� � Cursor$
 Hand��$��7��$��29��$��Bottom=�$ 6���� -   $ 	btnCancel��$��Cancelar��$��Right=�$��	0,0,16,10q�$��91��$
 Hand��$��8��$��29��$��Bottom=�$ 7��