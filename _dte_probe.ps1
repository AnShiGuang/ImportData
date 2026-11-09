$log = 'D:\批量导入数据重制版\_dte_probe.log'
"=== DTE probe $(Get-Date -Format o) ===" | Out-File -FilePath $log -Encoding utf8
foreach ($progid in @('VisualStudio.DTE.18.0','VisualStudio.DTE.17.0','VisualStudio.DTE')) {
  try {
    $dte = [System.Runtime.InteropServices.Marshal]::GetActiveObject($progid)
  } catch {
    "--- $progid : GetActiveObject failed: $($_.Exception.Message)" | Out-File -FilePath $log -Append -Encoding utf8
    continue
  }
  "--- $progid : attached" | Out-File -FilePath $log -Append -Encoding utf8
  try { "    Solution.FullName = " + $dte.Solution.FullName | Out-File -FilePath $log -Append -Encoding utf8 } catch { "    Solution.FullName error: $($_.Exception.Message)" | Out-File -FilePath $log -Append -Encoding utf8 }
  try { "    Solution.IsOpen   = " + $dte.Solution.IsOpen | Out-File -FilePath $log -Append -Encoding utf8 } catch { "    Solution.IsOpen error" | Out-File -FilePath $log -Append -Encoding utf8 }
  try {
    $i = 0
    foreach ($p in $dte.Solution.Projects) {
      $i++
      $name = try { $p.Name } catch { "<err>" }
      $kind = try { $p.Kind } catch { "<err>" }
      $file = try { $p.FileName } catch { "<err>" }
      $obj  = try { if ($null -eq $p.Object) { '<Object=null>' } else { '<Object=present>' } } catch { "<err>" }
      "    [$i] name=$name" | Out-File -FilePath $log -Append -Encoding utf8
      "        kind=$kind" | Out-File -FilePath $log -Append -Encoding utf8
      "        file=$file" | Out-File -FilePath $log -Append -Encoding utf8
      "        $obj" | Out-File -FilePath $log -Append -Encoding utf8
    }
    "    project count = $i" | Out-File -FilePath $log -Append -Encoding utf8
  } catch {
    "    Projects enumeration error: $($_.Exception.Message)" | Out-File -FilePath $log -Append -Encoding utf8
  }
  try {
    $win = $dte.MainWindow
    "    MainWindow.Title = " + $win.Caption | Out-File -FilePath $log -Append -Encoding utf8
  } catch { "    MainWindow error" | Out-File -FilePath $log -Append -Encoding utf8 }
  try { [System.Runtime.InteropServices.Marshal]::ReleaseComObject($dte) | Out-Null } catch {}
}
"=== done ===" | Out-File -FilePath $log -Append -Encoding utf8
