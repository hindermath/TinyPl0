(*** PL/0 Lehr-Compiler ***
 *
 * Kompilierbar mit DELPHI 10.4.x und 11 sowie LAZARUS
 *)
{$IFDEF FPC}
program pl0(input,output);
{$ELSE}
program pl0;
{$ENDIF}

{$IFDEF FPC}
  {$MODE Delphi}
{$ENDIF}


{$APPTYPE CONSOLE}

{$IFNDEF FPC}
{$R *.res}
{$ENDIF}

uses
  {$IFNDEF FPC}
  System.SysUtils,
  {$ELSE}
  SysUtils,
  {$ENDIF }
  pl0com in 'pl0com.pas';

var
  ch: char;
  cxcnt: Integer; { Code array counter }

{ pl/0 compiler with code generation }

procedure error(n: integer);
begin
  ExitCode := n; { Set exit code for possible program termination }
  if FindCmdLineSwitch('errmsg',True) then
    writeln(ErrOutput,'Error', ' ':cc - 1, '^', n:2, ': '+errortxt[n])
  else
    writeln(ErrOutput,'Error', ' ':cc - 1, '^', n:2);

  err := err + 1
end { error };

procedure getsym;
var
  i, j, k: integer;

  procedure getch;
  begin
    if cc = ll then
    begin
      if eof(input) then
      begin
        ExitCode := 98;
        writeln(ErrOutput, errortxt[ExitCode]);
        RunError(ExitCode);
      end;
      ll := 0;
      cc := 0;
      write(ErrOutput,cx:5, ' ');
      while not eoln(input) do
      begin
        ll := ll + 1;
        read(ch);
        write(ErrOutput,ch);
        line[ll] := ch
      end;
      writeln(ErrOutput);
      readln;
      ll := ll + 1;
      line[ll] := ' ';
    end;
    cc := cc + 1;
    ch := line[cc]
  end { getch };

begin { getsym }
  while ch = ' ' do
    getch;
  if ch in ['a' .. 'z'] then
  begin { identifier or reserved word }
    k := 0;
    repeat
      if k < al then
      begin
        k := k + 1;
        a[k] := ch
      end;
      getch;
    until not(ch in ['a' .. 'z', '0' .. '9']);
    if k >= kk then
      kk := k
    else
      repeat
        a[kk] := ' ';
        kk := kk - 1
      until kk = k;
    id := a;
    i := 1;
    j := norw;
    repeat
      k := (i + j) div 2;
      if id <= word[k] then
        j := k - 1;
      if id >= word[k] then
        i := k + 1 until i > j;
      if i - 1 > j then
        sym := wsym[k]
      else
        sym := ident
    end
  else if ch in ['0' .. '9'] then
  begin { number }
    k := 0;
    num := 0;
    sym := number;
    repeat
      num := 10 * num + (ord(ch) - ord('0'));
      k := k + 1;
      getch
    until not(ch in ['0' .. '9']);
    if k > nmax then
      error(30)
  end
  else if ch = ':' then
  begin
    getch;
    if ch = '=' then
    begin
      sym := becomes;
      getch
    end
    else
      sym := nul;
  end
  else
  begin
    sym := ssym[ch];
    getch
  end
end { getsym };

procedure test(s1, s2: symset; n: integer);
begin
  if not(sym in s1) then
  begin
    error(n);
    s1 := s1 + s2;
    while not(sym in s1) do
      getsym
  end
end { test };

procedure block(lev, tx: integer; fsys: symset);
var
  dx: integer; { data allocation index }
  tx0: integer; { initial table index }
  cx0: integer; { initial code index }
  procedure enter(k: pl0object);
  begin { enter pl0object into table }
    tx := tx + 1;
    with table[tx] do
    begin
      name := id;
      kind := k;
      case k of
        constant:
          begin
            if num > amax then
            begin
              error(30);
              num := 0
            end;
            val := num
          end;
        varible:
          begin
            level := lev;
            adr := dx;
            dx := dx + 1;
          end;
        proc:
          level := lev
      end
    end
  end { enter };

  function position(id: alfa): integer;
  var
    i: integer;
  begin { find indentifier id in table }
    table[0].name := id;
    i := tx;
    while table[i].name <> id do
      i := i - 1;
    position := i
  end { position };

  procedure constdeclaration;
  begin
    if sym = ident then
    begin
      getsym;
      if sym in [eql, becomes] then
      begin
        if sym = becomes then
          error(1);
        getsym;
        if sym = number then
        begin
          enter(constant);
          getsym
        end
        else
          error(2)
      end
      else
        error(3)
    end
    else
      error(4)
  end { constdeclaration };

  procedure vardeclaration;
  begin
    if sym = ident then
    begin
      enter(varible);
      getsym
    end
    else
      error(4)
  end { vardeclaration };

  procedure listcode;
  var
    i: integer;

  begin { list code generated for this block }
    for i := cx0 to cx - 1 do
    begin
      with code[i] do
        begin
            {$IFDEF FPC}
              if FindCmdLineSwitch('wopcod', True)	 then
                writeln(ErrOutput, i:5, '|',Integer(f):5, l:3, a:5,'|',mnemonic[f]:5, l:3, a:5)
              else
                writeln(ErrOutput, i:5, mnemonic[f]:5, l:3, a:5);
            {$ELSE}
              if FindCmdLineSwitch('wopcod', True)	 then
                writeln(ErrOutput, i:5, '|',Integer(f):5, l:3, a:5,'|',String(mnemonic[f]):5, l:3, a:5)
              else
                writeln(ErrOutput, i:5, String(mnemonic[f]):5, l:3, a:5);
            {$ENDIF}
        end;
    end;
  end { listcode };

  procedure statement(fsys: symset);
  var
    i, cx1, cx2: integer;
    procedure expression(fsys: symset);
    var
      addop: symbol;
      procedure term(fsys: symset);
      var
        mulop: symbol;
        procedure factor(fsys: symset);
        var
          i: integer;
        begin
          test(facbegsys, fsys, 24);
          while sym in facbegsys do
          begin
            if sym = ident then
            begin
              i := position(id);
              if i = 0 then
                error(11)
              else
                with table[i] do
                  case kind of
                    constant:
                      gen(lit, 0, val);
                    varible:
                      gen(lod, lev - level, adr);
                    proc:
                      error(21)
                  end;
              getsym
            end
            else if sym = number then
            begin
              if num > amax then
              begin
                error(30);
                num := 0
              end;
              gen(lit, 0, num);
              getsym
            end
            else if sym = lparen then
            begin
              getsym;
              expression([rparen] + fsys);
              if sym = rparen then
                getsym
              else
                error(22)
            end;
            test(fsys, [lparen], 23)
          end
        end { factor };

      begin { term }
        factor(fsys + [times, slash]);
        while sym in [times, slash] do
        begin
          mulop := sym;
          getsym;
          factor(fsys + [times, slash]);
          if mulop = times then
            gen(opr, 0, 4)
          else
            gen(opr, 0, 5)
        end
      end { term };

    begin { expression }
      if sym in [plus, minus] then
      begin
        addop := sym;
        getsym;
        term(fsys + [plus, minus]);
        if addop = minus then
          gen(opr, 0, 1)
      end
      else
        term(fsys + [plus, minus]);
      while sym in [plus, minus] do
      begin
        addop := sym;
        getsym;
        term(fsys + [plus, minus]);
        if addop = plus then
          gen(opr, 0, 2)
        else
          gen(opr, 0, 3)
      end
    end { expression };

    procedure condition(fsys: symset);
    var
      relop: symbol;
    begin
      if sym = oddsym then
      begin
        getsym;
        expression(fsys);
        gen(opr, 0, 6)
      end
      else
      begin
        expression([eql, neq, lss, gtr, leq, geq] + fsys);
        if not(sym in [eql, neq, lss, leq, gtr, geq]) then
          error(20)
        else
        begin
          relop := sym;
          getsym;
          expression(fsys);
          case relop of
            eql:
              gen(opr, 0, 8);
            neq:
              gen(opr, 0, 9);
            lss:
              gen(opr, 0, 10);
            geq:
              gen(opr, 0, 11);
            gtr:
              gen(opr, 0, 12);
            leq:
              gen(opr, 0, 13);
          end
        end
      end
    end { condition };

  begin { statement }
    if sym = ident then
    begin
      i := position(id);
      if i = 0 then
        error(11)
      else if table[i].kind <> varible then
      begin { assignment to non-varible }
        error(12);
        i := 0
      end;
      getsym;
      if sym = becomes then
        getsym
      else
        error(13);
      expression(fsys);
      if i <> 0 then
        with table[i] do
          gen(sto, lev - level, adr)
    end
    else if sym = callsym then
    begin
      getsym;
      if sym <> ident then
        error(14)
      else
      begin
        i := position(id);
        if i = 0 then
          error(11)
        else
          with table[i] do
            if kind = proc then
              gen(cal, lev - level, adr)
            else
              error(15);
        getsym
      end
    end
    else if sym = ifsym then
    begin
      getsym;
      condition([thensym, dosym] + fsys);
      if sym = thensym then
        getsym
      else
        error(16);
      cx1 := cx;
      gen(jpc, 0, 0);
      statement(fsys);
      code[cx1].a := cx
    end
    else if sym = beginsym then
    begin
      getsym;
      statement([semicolon, endsym] + fsys);
      while sym in [semicolon] + statbegsys do
      begin
        if sym = semicolon then
          getsym
        else
          error(10);
        statement([semicolon, endsym] + fsys)
      end;
      if sym = endsym then
        getsym
      else
        error(17)
    end
    else if sym = whilesym then
    begin
      cx1 := cx;
      getsym;
      condition([dosym] + fsys);
      cx2 := cx;
      gen(jpc, 0, 0);
      if sym = dosym then
        getsym
      else
        error(18);
      statement(fsys);
      gen(jmp, 0, cx1);
      code[cx2].a := cx
    end;
    test(fsys, [], 19)
  end { statement };

begin { block }
  dx := 3;
  tx0 := tx;
  table[tx].adr := cx;
  gen(jmp, 0, 0);
  if lev > levmax then
    error(32);
  repeat
    if sym = constsym then
    begin
      getsym;
      repeat
        constdeclaration;
        while sym = comma do
        begin
          getsym;
          constdeclaration
        end;
        if sym = semicolon then
          getsym
        else
          error(5) until sym <> ident
      end;
      if sym = varsym then
      begin
        getsym;
        repeat
          vardeclaration;
          while sym = comma do
          begin
            getsym;
            vardeclaration
          end;
          if sym = semicolon then
            getsym
          else
            error(5) until sym <> ident;
        end;
        while sym = procsym do
        begin
          getsym;
          if sym = ident then
          begin
            enter(proc);
            getsym
          end
          else
            error(4);
          if sym = semicolon then
            getsym
          else
            error(5);
          block(lev + 1, tx, [semicolon] + fsys);
          if sym = semicolon then
          begin
            getsym;
            test(statbegsys + [ident, procsym], fsys, 6)
          end
          else
            error(5)
        end;
        test(statbegsys + [ident], declbegsys, 7)
        until not(sym in declbegsys);
        code[table[tx0].adr].a := cx;
        with table[tx0] do
        begin
          adr := cx; { start adr of code }
        end;
        cx0 := 0 { cx };
        gen(ink, 0, dx);
        statement([semicolon, endsym] + fsys);
        gen(opr, 0, 0); { return }
        test(fsys, [], 8);
        listcode;
      end { block };

procedure init;
begin
  for ch := chr(0) to chr(255) do
    ssym[ch] := nul;
  word[1] := 'begin     ';
  word[2] := 'call      ';
  word[3] := 'const     ';
  word[4] := 'do        ';
  word[5] := 'end       ';
  word[6] := 'if        ';
  word[7] := 'odd       ';
  word[8] := 'procedure ';
  word[9] := 'then      ';
  word[10] := 'var       ';
  word[11] := 'while     ';
  wsym[1] := beginsym;
  wsym[2] := callsym;
  wsym[3] := constsym;
  wsym[4] := dosym;
  wsym[5] := endsym;
  wsym[6] := ifsym;
  wsym[7] := oddsym;
  wsym[8] := procsym;
  wsym[9] := thensym;
  wsym[10] := varsym;
  wsym[11] := whilesym;
  ssym['+'] := plus;
  ssym['-'] := minus;
  ssym['*'] := times;
  ssym['/'] := slash;
  ssym['('] := lparen;
  ssym[')'] := rparen;
  ssym['='] := eql;
  ssym[','] := comma;
  ssym['.'] := period;
  ssym['#'] := neq;
  ssym['<'] := lss;
  ssym['>'] := gtr;
  ssym['['] := leq;
  ssym[']'] := geq;
  ssym[';'] := semicolon;
  mnemonic[lit] := '  lit';
  mnemonic[opr] := '  opr';
  mnemonic[lod] := '  lod';
  mnemonic[sto] := '  sto';
  mnemonic[cal] := '  cal';
  mnemonic[ink] := '  ink';
  mnemonic[jmp] := '  jmp';
  mnemonic[jpc] := '  jpc';

  stdoutmnemonic[lit] := 'lit';
  stdoutmnemonic[opr] := 'opr';
  stdoutmnemonic[lod] := 'lod';
  stdoutmnemonic[sto] := 'sto';
  stdoutmnemonic[cal] := 'cal';
  stdoutmnemonic[ink] := 'ink';
  stdoutmnemonic[jmp] := 'jmp';
  stdoutmnemonic[jpc] := 'jpc';

  errortxt[1] := 'Use = instead of :=.';
  errortxt[2] := '= must be followd by a number.';
  errortxt[2] := 'Identifier must be followd by a =.';
  errortxt[4] := '*const*, *var*, *procedure* must be follows by an indentifier.';
  errortxt[5] := 'Semicolon or comma missing';
  errortxt[6] := 'Incorrect symbol after procedure declaration.';
  errortxt[7] := 'Statement expected.';
  errortxt[8] := 'Incorrect symbol after statement part in block.';
  errortxt[9] := 'Perid expected.';
  errortxt[10] := 'Semicolon between statements missing.';
  errortxt[11] := 'Undeclared identifier.';
  errortxt[12] := 'Assignment to constant or procedure is not allowd.';
  errortxt[13] := 'Assignment operator := expected.';
  errortxt[14] := '*call* must be folloed by an identifier.';
  errortxt[15] := 'Call of a constant or a variable is meaningless.';
  errortxt[16] := '*then* expected.';
  errortxt[17] := 'Semicolon or *end* expected.';
  errortxt[18] := '*do* expected.';
  errortxt[19] := 'Incorrect symbol following statement.';
  errortxt[20] := 'Relational operator expected.';
  errortxt[21] := 'Expression must not contain a procedure identifier.';
  errortxt[22] := 'Right parenthesis missing.';
  errortxt[23] := 'The preceding factor cannot be followd by this symbol.';
  errortxt[24] := 'An expression cannot begin with this symbol.';
  errortxt[30] := 'This number is too large';
  errortxt[96] := 'No emitter switches [-|/]asm or [-|/]cod found.';
  errortxt[97] := 'Errors in PL/0 program!';
  errortxt[98] := 'Program incomplete!';
  errortxt[99] := 'Unexpected termination!';
  declbegsys := [constsym, varsym, procsym];
  statbegsys := [beginsym, callsym, ifsym, whilesym];
  facbegsys := [ident, number, lparen];
  err := 0;
  ExitCode := err;  { Set exit code for possible program termination }
  cc := 0;
  cx := 0;
  ll := 0;
  ch := ' ';
  kk := al;
end; { init }

procedure PrintUsage(errcode: integer);
begin
  ExitCode := errcode;
  writeln(ErrOutput, 'Usage:');
  writeln(ErrOutput, ExtractFileName(ParamStr(0))+' [-|/]h | [-|/]? | [-|/]help - print this information');
  writeln(ErrOutput, ExtractFileName(ParamStr(0))+' [-|/]errmsg - show long compiler error messages');
  writeln(ErrOutput, ExtractFileName(ParamStr(0))+' [-|/]wopcod - writes additionaly the op codes of the instruction');
  writeln(ErrOutput, ExtractFileName(ParamStr(0))+' [-|/]conly - compile only and do not interpret');
  writeln(ErrOutput, ExtractFileName(ParamStr(0))+' [-|/]emit {[-|/]asm | [-|/]cod} - emit PL0 assembler instructions or opcodes to STDOUT');
  Halt(ExitCode)
end;

begin { main program }
  if FindCmdLineSwitch('?') or FindCmdLineSwitch('h', True) or FindCmdLineSwitch('help', True) then
    PrintUsage(99);

  try
    init;
    getsym;
    block(0, 0, [period] + declbegsys + statbegsys);
    if sym <> period then
      error(9);
    if (err = 0) and (not FindCmdLineSwitch('conly', True)) then
      begin
        ExitCode := err;
        interpret;
      end;

    if (err = 0) and FindCmdLineSwitch('emit', True) then
        begin
          ExitCode := err;
          for cxcnt := 0 to cx-1 do
            begin
              with code[cxcnt] do
                begin
                  if FindCmdLineSwitch('asm', True) then
                    begin
                      {$IFDEF FPC}
                        writeln(stdoutmnemonic[f],' ', l,' ', a)
                      {$ELSE}
                        writeln(String(stdoutmnemonic[f]),' ', l,' ', a)
                      {$ENDIF}
                    end
                  else if FindCmdLineSwitch('cod', True) then
                     begin
                        writeln(Integer(f),' ', l,' ', a)
                     end
                  else
                    begin
                      ExitCode := 96;
                      writeln(ErrOutput, errortxt[ExitCode]);
                      RunError(ExitCode)
                    end
                end
            end
        end;

      if err > 0 then
        begin
          ExitCode := 97;
          writeln(ErrOutput,errortxt[ExitCode]);
          RunError(ExitCode)
        end;
  except
    on E: Exception do
      writeln(ErrOutput,E.ClassName, ': ', E.Message, '-> ', errortxt[99]);
  end;

end.
