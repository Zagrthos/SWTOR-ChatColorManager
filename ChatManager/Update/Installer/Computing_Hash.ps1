$file = 'C:\_InnoSetup\Output\SWTOR-ChatManager-v1.5.0.exe'
$hash = Get-FileHash -Path $file -Algorithm SHA256
$hash.Hash