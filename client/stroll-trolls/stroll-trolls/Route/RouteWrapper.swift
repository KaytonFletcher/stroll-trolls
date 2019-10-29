//
//  RouteWrapper.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/27/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import Foundation
import SwiftUI
import UIKit


struct RouteWrapper: UIViewControllerRepresentable {
        @Binding var data: [RouteData]?
       
      func makeUIViewController(context: Context) -> RouteNavController {
        let routeNavController = RouteNavController(coordinates: data!)
          return routeNavController
      }
      
      func updateUIViewController(_ uiViewController: RouteNavController, context: Context) {
          
      }

}
