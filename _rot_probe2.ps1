$ErrorActionPreference = 'Continue'
$log = 'D:\批量导入数据重制版\_rot2.log'
Remove-Item $log -ErrorAction SilentlyContinue

try { [System.Threading.Thread]::CurrentThread.SetApartmentState([System.Threading.ApartmentState]::STA) } catch {}

$src = @'
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

public static class RotDump2
{
    [DllImport("ole32.dll")]
    public static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

    public static string DumpAll()
    {
        var sb = new StringBuilder();
        IRunningObjectTable rot;
        int hr = GetRunningObjectTable(0, out rot);
        if (hr != 0) { sb.AppendLine("GetRunningObjectTable hr=0x" + hr.ToString("X8")); return sb.ToString(); }
        IEnumMoniker enumMk;
        hr = rot.EnumRunning(out enumMk);
        sb.AppendLine("EnumRunning hr=0x" + hr.ToString("X8"));
        if (hr != 0) return sb.ToString();
        IMoniker[] mks = new IMoniker[1];
        IntPtr fetched = IntPtr.Zero;
        int total = 0;
        while (enumMk.Next(1, mks, fetched) == 0)
        {
            total++;
            string name = null;
            try { mks[0].GetDisplayName(null, null, out name); } catch (Exception e) { name = "<err:" + e.Message + ">"; }
            sb.AppendLine(total + ": " + (name ?? "<null>"));
        }
        sb.AppendLine("TOTAL monikers: " + total);
        return sb.ToString();
    }
}
'@

try {
  Add-Type -TypeDefinition $src -Language CSharp -ErrorAction Stop
  [RotDump2]::DumpAll() | Out-File -FilePath $log -Encoding utf8
} catch {
  ('FAIL: ' + $_.Exception.Message) | Out-File -FilePath $log -Encoding utf8
}
Get-Content $log -ErrorAction SilentlyContinue
