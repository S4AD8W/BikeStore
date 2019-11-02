

var JsCart = {

	ChangeProductQuantiti(xInput) {

		$.post("/Cart/ChangeQuantiti", {
			xIdxProduct: xInput[0].dataset.idxproduct,
			xQuantiti: xInput[0].value
		})
			.done(xData => {
				window.location.reload();
			})
			.fail(xData => {

			});

	},

	RemoveItem(xEvent) {

		$.post("/Cart/RemoveItem", {
			xIdxProduct: xEvent.currentTarget.dataset.idxproduct
			
		})
			.done(xData => {
				window.location.reload();
			})
			.fail(xData => {

			});

	}

};