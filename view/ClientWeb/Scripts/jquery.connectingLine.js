(function($) {
	$.fn.connect = function(param, canvasid) {

		var _canvas;
		var _ctx;
		var _lines = new Array();
		var _me = this;
		var _parent = param || document;

		_canvas = $('canvas#' + canvasid);
		if (_canvas == null || _canvas.length == 0) {
		    _canvas = $('<canvas style="display:block;position:absolute;top:0;left:0;"/>');
		    _parent.append(_canvas);
		}
		_canvas.attr('width', _parent.width()).attr('height', _parent.height()).attr('id', canvasid || 'mycanvas');

		this.drawLine = function (option) {
		    _lines.push(option);
			this.connect(option);
		};
		
		this.connect = function(option) {
			_ctx = _canvas[0].getContext('2d');
			_ctx.beginPath();
			try {
				var _color;
				var _dash;
				var _left = new Object();
				var _right = new Object(); 
				var _error = (option.error == 'show') || false;
				var _par = option.sub || 0;
				var _ileft = _canvas.position().left;
				var _itop = _canvas.position().top;
				if (option.left_node != '' && typeof option.left_node !== 'undefined' && option.right_node != '' && typeof option.right_node !== 'undefined' && $(option.left_node).length > 0 && $(option.right_node).length > 0) {
					switch (option.status) {
						case 'accepted':
							_color = '#0969a2';
							break;
						case 'rejected':
							_color = '#e7005d';
							break;
						case 'modified':
							_color = '#bfb230';
							break;
						case 'none':
							_color = 'grey';
							break;
						default:
							_color = 'red';
							break;
					}

					switch (option.style) {
						case 'dashed':
							_dash = [4, 2];
							break;
						case 'solid':
							_dash = [0, 0];
							break;
						case 'dotted':
							_dash = [4, 2];
							break;
						default:
							_dash = [4, 2];
							break;
					}

					$(option.right_node).each(function (index, value) {
						_left_node = $(option.left_node);
						_right_node = $(value);
						if (_left_node.position().left >= _right_node.position().left) {
							_tmp = _left_node
							_left_node = _right_node
							_right_node = _tmp;
						}

						_left.x = _left_node.position().left + _left_node.outerWidth() - _ileft;
						_left.y = _left_node.position().top + (_left_node.outerHeight() / 2) + _par - _itop;
						_right.x = _right_node.position().left - _ileft;
						_right.y = _right_node.position().top + (_right_node.outerHeight() / 2) + _par - _itop;

						//Draw Line
						var _gap = option.horizantal_gap || 0;


						_ctx.moveTo(_left.x, _left.y);
						if (_gap != 0) {
							_ctx.lineTo(_left.x + _gap, _left.y);
							_ctx.lineTo(_right.x - _gap, _right.y);
						}
						_ctx.lineTo(_right.x, _right.y);

						if (!_ctx.setLineDash) {
							_ctx.setLineDash = function() {}
						} else {
							_ctx.setLineDash(_dash);
						}
						_ctx.lineWidth = option.width || 2;
						_ctx.strokeStyle = _color;
						_ctx.stroke();
					});
				} else {
				}
			} catch (err) {
			}
		};
        		
		$(window).resize(function() {
			_me.redrawLines();
		});

		this.redrawLines = function() {
			_ctx.clearRect(0, 0, $(_parent).width(), $(_parent).height());
			_lines.forEach(function(entry) {
				entry.resize = true;
				_me.connect(entry);
			});
		};

		return this;
	};

	$.fn.disconnect = function (param, camvasid) {
	    var _parent = param || document;
	    var _canvas = $('canvas#' + camvasid);
	    if (_canvas.length > 0) {
	        var _ctx = _canvas[0].getContext('2d');
	        _ctx.clearRect(0, 0, $(_parent).width(), $(_parent).height());
	        _canvas.remove();
	    }
	}
}(jQuery));