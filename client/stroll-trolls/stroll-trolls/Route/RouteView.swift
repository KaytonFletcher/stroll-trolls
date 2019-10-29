//
//  RouteView.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/27/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import SwiftUI

struct RouteView: View {
     @Binding var data: [RouteData]?
    
    
    var body: some View {
        RouteWrapper(data: $data)
    }
}

//struct RouteView_Previews: PreviewProvider {
//    static var previews: some View {
//        RouteView(data: Binding<[RouteData]>)
//    }
//}

