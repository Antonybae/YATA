python -m compileall main.py
ren  __pycache__  Release
cd Release
ren main.cpython-36.pyc application.pyc
pause