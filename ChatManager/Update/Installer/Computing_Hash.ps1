$file = 'C:\_InnoSetup\Output\SWTOR-ChatManager-v1.4.2.exe'
$hash = Get-FileHash -Path $file -Algorithm SHA256
$hash.Hash