   M S B A M L   `   `   `  ����M  IGraphBaseFramewark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:GraphBaseFramewark;assembly=GraphBaseFramewarkGraphBaseFramewark  5      P LGraphNQueensAlgorithm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:GraphNQueensAlgorithmGraphNQueensAlgorithm 5      .   (GraphNQueensAlgorithm.GraphNQueensCanvas   5      SGraphBaseFramewark<clr-namespace:GraphBaseFramewark;assembly=GraphBaseFramewark  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:GraphNQueensAlgorithm     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��9* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   .+�5   
   �� $
��Auto��6   $
��Auto��6%   $��Visible=�5      $��Visible=�5      .��5      �� -   5         ��Name$ stackPanelFather��5   4   $
��Auto��6   $
��Auto��6'   .+�5      $   GraphBaseFramewark.GraphCanvas  -   5          Name$ ucGraphCanvas��5   1   $��0,0,0,0q�6H   5      5      �� 5   
   $��皇后个数：��6   $
��Left=�6    $��	10,10,0,0q�6;   $��1��6N   $	��Top=�6[   �� 5   
   -   5         ��Name$ tbQueensNodeCount��5      $��80,2,0,0q�6-   $
��Left=�6?   $��20��6Z   $��80��6f   $��1��6q   $��8��6~   �� 5   
   -   5         ��Name$ btnCreateRelNode��5      $��生成关系图��6+   $
��Left=�6;   $��	10,13,0,0q�6V   $��2��6i   $	��Top=�6v   $��75��6�   6�   �� 5   
   -   5        $ btnReCallBack��5      $��回溯算法��6(   $
��Left=�67   $��
110,13,0,0q�6R   $��2��6f   $	��Top=�6s   $��75��6�   6�   5      5      