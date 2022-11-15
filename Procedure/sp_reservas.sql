USE [db_reservas]
GO

/****** Object:  StoredProcedure [dbo].[sp_reservas]    Script Date: 08/11/2022 10:25:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create or alter procedure [dbo].[sp_reservas](
@i_accion				char(1) = null,
@i_tipo_dato			char(1) = null,
@i_id_usuario			bigint	= null,
@i_nombre_usuario		varchar(100) = null,
@i_apellido_usuario		varchar(100) = null,
@i_correo_usuario		varchar(100) = null,
@i_telefono_usuario		varchar(10) = null,
@i_estado				varchar(1) = null,
@i_id_tipo_evento		bigint = null,
@i_nombre_evento		varchar(100) = null,
@i_costo_evento			decimal(20,2) = null,
@i_id_tipo_servicio		bigint = null,
@i_nombre_servicio		varchar(100) = null,
@i_costo_servicio		decimal(20,2) = null,
@i_id_reserva			bigint = null,
@i_cantidad_camarografos	int = null,
@i_fecha_hora_evento	datetime = null,
@i_duracion_evento		int = null,
@i_direccion_evento		varchar(200) = null
)
as
begin
	if @i_accion = 'G'
	begin
		if @i_tipo_dato = 'U'
		begin
			select
				id_usuario,
				nombre,
				apellido,
				correo,
				telefono,
				estado,
				fecha_reg,
				fecha_mod
			from
				usuario
			where
				estado = 'A'
		end
		if @i_tipo_dato = 'S'
		begin
			select
				id_tipo_servicio,
				nombre as nombre_servicio,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_servicio
			where
				estado = 'A'
		end
		if @i_tipo_dato = 'E'
		begin
			select
				id_tipo_evento,
				nombre as nombre_evento,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_evento
			where
				estado = 'A'
		end
		if @i_tipo_dato = 'R'
		begin
			select
				id_reserva,
				u.nombre as nombre_usuario,
				ts.nombre as nombre_servicio,
				te.nombre as nombre_evento,
				cantidad_camarografos ,
				duracion_evento ,
				costo_total,
				fecha_hora_evento,
				direccion_evento,
				r.estado as estado,
				r.fecha_reg as fecha_reg,
				r.fecha_mod as fecha_mod
			from 
				reserva as r, 
				usuario as u,
				tipo_evento as te,
				tipo_servicio as ts
			where
				u.id_usuario = r.usuario_id and
				te.id_tipo_evento = r.tipo_evento_id and
				ts.id_tipo_servicio = r.tipo_servicio_id and
				r.estado = 'A'
		end
	end
	if @i_accion = 'I'
	begin
		if @i_tipo_dato = 'U'
		begin
			insert into
				usuario(
					nombre,
					apellido,
					correo,
					telefono,
					estado,
					fecha_reg
				)
			values(
				@i_nombre_usuario,
				@i_apellido_usuario,
				@i_correo_usuario,
				@i_telefono_usuario,
				@i_estado,
				GETDATE()
			)
			select
				top 1
				id_usuario,
				nombre,
				apellido,
				correo,
				telefono,
				estado,
				fecha_reg,
				fecha_mod
			from
				usuario
			order by
				id_usuario desc
		end
		if @i_tipo_dato = 'E'
		begin
			insert into
				tipo_evento(
					nombre,
					costo,
					estado,
					fecha_reg
				)
			values(
				@i_nombre_evento,
				@i_costo_evento,
				@i_estado,
				GETDATE()
			)
			select 
				top 1
				id_tipo_evento,
				nombre as nombre_evento,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_evento
			order by
				id_tipo_evento desc
		end
		if @i_tipo_dato = 'S'
		begin
			insert into
				tipo_servicio(
					nombre,
					costo,
					estado,
					fecha_reg
				)
			values(
				@i_nombre_servicio,
				@i_costo_servicio,
				@i_estado,
				GETDATE()
			)
			select 
				top 1
				id_tipo_servicio,
				nombre as nombre_servicio,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_servicio
			order by
				id_tipo_servicio desc
		end
		if @i_tipo_dato = 'R'
		begin
			declare @i_costo_total decimal(20,2); 
			set @i_costo_total =(select te.costo+ts.costo from tipo_evento as te,tipo_servicio as ts
			where te.id_tipo_evento = @i_id_tipo_evento and	ts.id_tipo_servicio = @i_id_tipo_servicio);
			insert into 
				reserva(
					usuario_id,
					tipo_servicio_id,
					tipo_evento_id,
					cantidad_camarografos ,
					duracion_evento ,
					costo_total,
					fecha_hora_evento,
					direccion_evento,
					estado,
					fecha_reg
				)
			values(
				@i_id_usuario,
				@i_id_tipo_servicio,
				@i_id_tipo_evento,
				@i_cantidad_camarografos,
				@i_duracion_evento,
				@i_costo_total,
				@i_fecha_hora_evento,
				@i_direccion_evento,
				@i_estado,
				GETDATE()
			)
			select
				top 1
				id_reserva,
				u.nombre as nombre_usuario,
				ts.nombre as nombre_servicio,
				te.nombre as nombre_evento,
				cantidad_camarografos ,
				duracion_evento ,
				costo_total,
				fecha_hora_evento,
				direccion_evento,
				r.estado as estado,
				r.fecha_reg as fecha_reg,
				r.fecha_mod as fecha_mod
			from 
				reserva as r, 
				usuario as u,
				tipo_evento as te,
				tipo_servicio as ts
			where
				u.id_usuario = @i_id_usuario and
				te.id_tipo_evento = @i_id_tipo_evento and
				ts.id_tipo_servicio = @i_id_tipo_servicio
			order by
				id_reserva desc
		end
	end
	if @i_accion = 'U'
	begin
		if @i_tipo_dato = 'U'
		begin
			update
				usuario
			set
				nombre = ISNULL(@i_nombre_usuario, nombre),
				apellido = ISNULL(@i_apellido_usuario,apellido),
				correo = ISNULL(@i_correo_usuario, correo),
				telefono = ISNULL(@i_telefono_usuario, telefono),
				estado = ISNULL(@i_estado, estado),
				fecha_mod = GETDATE()
			where
				id_usuario = @i_id_usuario
			select
				id_usuario,
				nombre,
				apellido,
				correo,
				telefono,
				estado,
				fecha_reg,
				fecha_mod
			from
				usuario
			where
				id_usuario = @i_id_usuario
		end
		if @i_tipo_dato = 'S'
		begin
			update
				tipo_servicio
			set
				nombre = ISNULL(@i_nombre_servicio, nombre),
				costo = ISNULL(@i_costo_servicio, costo),
				estado = ISNULL(@i_estado, estado),
				fecha_mod = GETDATE()
			where
				id_tipo_servicio = @i_id_tipo_servicio
			select
				id_tipo_servicio,
				nombre as nombre_servicio,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_servicio
			where
				id_tipo_servicio = @i_id_tipo_servicio
		end
		if @i_tipo_dato = 'E'
		begin
			update
				tipo_evento
			set
				nombre = ISNULL(@i_nombre_evento, nombre),
				costo = ISNULL(@i_costo_evento, costo),
				estado = ISNULL(@i_estado, estado),
				fecha_mod = GETDATE()
			where
				id_tipo_evento = @i_id_tipo_evento
			select
				id_tipo_evento,
				nombre as nombre_evento,
				costo,
				estado,
				fecha_reg,
				fecha_mod
			from
				tipo_evento
			where
				id_tipo_evento = @i_id_tipo_evento
		end
		if @i_tipo_dato = 'R'
		begin
			update
				reserva
			set
				usuario_id = ISNULL(@i_id_usuario, usuario_id),
				tipo_evento_id = ISNULL(@i_id_tipo_evento, tipo_evento_id),
				tipo_servicio_id = ISNULL(@i_id_tipo_servicio, tipo_servicio_id),
				cantidad_camarografos = ISNULL(@i_cantidad_camarografos, cantidad_camarografos),
				duracion_evento = ISNULL(@i_duracion_evento,duracion_evento),
				fecha_hora_evento = ISNULL(@i_fecha_hora_evento,fecha_hora_evento),
				direccion_evento = ISNULL(@i_direccion_evento, direccion_evento),
				estado = ISNULL(@i_estado,estado),
				fecha_mod = GETDATE(),
				costo_total = (select te.costo+ts.costo from tipo_evento as te,tipo_servicio as ts
			where te.id_tipo_evento = tipo_evento_id and	ts.id_tipo_servicio = tipo_servicio_id)
			where
				id_reserva = @i_id_reserva
			select
				id_reserva,
				u.nombre as nombre_usuario,
				ts.nombre as nombre_servicio,
				te.nombre as nombre_evento,
				cantidad_camarografos ,
				duracion_evento ,
				costo_total,
				fecha_hora_evento,
				direccion_evento,
				r.estado as estado,
				r.fecha_reg as fecha_reg,
				r.fecha_mod as fecha_mod
			from 
				reserva as r, 
				usuario as u,
				tipo_evento as te,
				tipo_servicio as ts
			where
				u.id_usuario = r.usuario_id and
				te.id_tipo_evento = r.tipo_evento_id and
				ts.id_tipo_servicio = r.tipo_servicio_id and
				id_reserva = @i_id_reserva
		end
	end
end
GO


