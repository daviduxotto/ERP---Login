use erp;
drop procedure login_user;
go
create procedure login_user
	@parusuario varchar(15),
	@parclave varchar(15),
	@parretorno int output,
	@parmensaje varchar(45) output,
	@partoken varchar(64) output
as 
begin
	declare @varclavecompuesta varchar(64);
	declare @vartoken varchar (64);

	set @varclavecompuesta= convert(varchar(64), HASHBYTES('SHA',@parclave),2);

	if not exists (select cod_usuario from mae_usuario where cod_usuario=@parusuario and clave = @varclavecompuesta) 
	begin
		set @parretorno=-1;
		set @parmensaje='Usuario o clave invalida';
		return;
	end

	set @vartoken =  convert(varchar(64), HASHBYTES('SHA', concat(@parusuario,getdate())),2);
	insert into det_token (token,cod_usuario,fecha_creacion,fecha_expiracion) values (@vartoken,@parusuario,getdate(),dateadd(hour,8,getdate()));
	set @parretorno=1;
	set @parmensaje='Bienvenido';
	set @partoken=@vartoken;
end
go