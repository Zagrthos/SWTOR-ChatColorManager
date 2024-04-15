$file = 'C:\_InnoSetup\ChatManager\Output\SWTOR-ChatManager-v1.6.0.exe'
$hash = Get-FileHash -Path $file -Algorithm SHA256
$hash.Hash